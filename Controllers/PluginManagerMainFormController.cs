using PpmMain.LocalInstaller;
using PpmMain.Models;
using PpmMain.PluginRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PpmMain.Controllers
{
    /// <summary>
    /// A custom comparer that determines whether two plugins are the same.
    /// </summary>
    internal class PluginComparer : IEqualityComparer<PluginDescription>
    {
        public bool Equals(PluginDescription x, PluginDescription y) => isSamePlugin(x, y);

        public int GetHashCode(PluginDescription obj) => obj.Name.GetHashCode();

        /// <summary>
        /// This method determines whether two plugins are the same.
        /// </summary>
        private readonly static Func<PluginDescription, PluginDescription, bool> isSamePlugin = (PluginDescription x, PluginDescription y) => x.Name == y.Name && x.ShortName == y.ShortName;

        /// <summary>
        /// This method determines whether the version of one plugin is greater than the version of another.
        /// </summary>
        internal readonly static Func<PluginDescription, PluginDescription, bool> isNewPluginVersion = (PluginDescription x, PluginDescription y) => isSamePlugin(x, y) && new Version(x.Version) > new Version(y.Version);
    }

    /// <summary>
    /// A controller which handles business logic for the main Plugin Manager form.
    /// </summary>
    class PluginManagerMainFormController : IPluginManagerMainFormController
    {
        IPluginRepository RemotePluginRepository { get; set; }

        IInstallerService LocalInstallerService { get; set; }

        public string FilterCriteria { get; set; }

        /// <summary>
        /// Determines whether a plugin should be displayed based on the filter criteria.
        /// </summary>
        private readonly Func<PluginDescription, string, bool> isNotFiltered = (PluginDescription plugin, string filterCriteria) =>
        {
            bool nameMatches = -1 != plugin.Name.IndexOf(filterCriteria, StringComparison.CurrentCultureIgnoreCase);
            bool versionMatches = -1 != plugin.Version.IndexOf(filterCriteria, StringComparison.CurrentCultureIgnoreCase); ;
            bool versionDescriptionMatches = -1 != plugin.VersionDescription.IndexOf(filterCriteria, StringComparison.CurrentCultureIgnoreCase); ;
            bool descriptionMatches = -1 != plugin.Description.IndexOf(filterCriteria, StringComparison.CurrentCultureIgnoreCase); ;
            return nameMatches || versionMatches || versionDescriptionMatches || descriptionMatches;
        };

        public List<PluginDescription> InstalledPlugins
        {
            get
            {
                return String.IsNullOrEmpty(FilterCriteria)
                    ? this._installedPlugins
                    : this._installedPlugins.Where(plugin => isNotFiltered(plugin, FilterCriteria)).ToList();
            }
            set
            {
                this._installedPlugins = value;
            }
        }
        private List<PluginDescription> _installedPlugins;

        List<PluginDescription> RemotePlugins
        {
            get
            {
                return String.IsNullOrEmpty(FilterCriteria)
                    ? this._remotePlugins
                    : this._remotePlugins.Where(plugin => isNotFiltered(plugin, FilterCriteria)).ToList();
            }
            set
            {
                this._remotePlugins = value;
            }
        }
        private List<PluginDescription> _remotePlugins;

        public List<PluginDescription> AvailablePlugins
        {
            get
            {
                return RemotePlugins.Except(this._installedPlugins, new PluginComparer()).ToList();
            }
            set => throw new NotImplementedException();
        }

        public List<OutdatedPlugin> OutdatedPlugins
        {
            get
            {
                /// Find existing plugins that have updates available
                Dictionary<PluginDescription, PluginDescription> outdated = new Dictionary<PluginDescription, PluginDescription>();
                this._installedPlugins.ForEach(installedPlugin =>
                {
                    PluginDescription remotePlugin = this._remotePlugins.Find(rp => PluginComparer.isNewPluginVersion(rp, installedPlugin));
                    if (null != remotePlugin)
                        outdated.Add(installedPlugin, remotePlugin);
                });

                /// Create a list of plugins that includes the existing version number and the available version number
                return outdated
                    .Where(PluginKvp => String.IsNullOrEmpty(FilterCriteria) || (isNotFiltered(PluginKvp.Key, FilterCriteria) || isNotFiltered(PluginKvp.Value, FilterCriteria)))
                    .Select(PluginKvp =>
                    {
                        PluginDescription installed = PluginKvp.Key;
                        PluginDescription available = PluginKvp.Value;

                        return new OutdatedPlugin
                        {
                            Name = installed.Name,
                            ShortName = installed.ShortName,
                            InstalledVersion = installed.Version,
                            Version = available.Version,
                            Description = available.Description,
                            VersionDescription = available.VersionDescription,
                            PtVersions = available.PtVersions,
                            License = available.License
                        };
                    }).ToList();
            }
            set => throw new NotImplementedException();
        }

        public void InstallPlugin(PluginDescription plugin)
        {
            FileInfo downloadedPlugin = RemotePluginRepository.DownloadPlugin(plugin);
            LocalInstallerService.InstallPlugin(downloadedPlugin);
            RefreshInstalled();
        }

        public void UninstallPlugin(PluginDescription plugin)
        {
            var onlyLatestPlugins = true;
            var onlyCompatiblePlugins = true;

            LocalInstallerService.UninstallPlugin(plugin);
            RemotePluginRepository.GetAvailablePlugins(onlyLatestPlugins, onlyCompatiblePlugins);
            RefreshInstalled();
        }

        public void UpdatePlugins(List<OutdatedPlugin> plugins)
        {
            plugins.ForEach(plugin => InstallPlugin(plugin));
        }

        public PluginManagerMainFormController()
        {
            string installPath = Path.Combine(Directory.GetCurrentDirectory(), "plugins");
            var onlyLatestPlugins = true;
            var onlyCompatiblePlugins = true;

            RemotePluginRepository = new S3PluginRepositoryService();
            LocalInstallerService = new LocalInstallerService(installPath);
            RemotePlugins = RemotePluginRepository.GetAvailablePlugins(onlyLatestPlugins, onlyCompatiblePlugins);
            RefreshInstalled();
        }

        /// <summary>
        /// This method handles updating the list of installed plugins.
        /// </summary>
        private void RefreshInstalled()
        {
            InstalledPlugins = LocalInstallerService.GetInstalledPlugins();
        }
    }
}
