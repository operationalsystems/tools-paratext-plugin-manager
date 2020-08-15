using PpmMain.LocalInstaller;
using PpmMain.Models;
using PpmMain.PluginRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PpmMain.Controllers
{
    internal class PluginComparer : IEqualityComparer<PluginDescription>
    {
        public bool Equals(PluginDescription x, PluginDescription y) => PluginManagerMainFormController.isSamePlugin(x, y);

        public int GetHashCode(PluginDescription obj) => obj.Name.GetHashCode();
    }
    class PluginManagerMainFormController : IPluginManagerMainFormController
    {
        IPluginRepository RemotePluginRepository { get; set; }

        IInstallerService LocalInstallerService { get; set; }
        public string FilterCriteria { get; set; }
        public List<PluginDescription> InstalledPlugins
        {
            get
            {
                return String.IsNullOrEmpty(FilterCriteria)
                    ? this._installedPlugins
                    : this._installedPlugins.Where(plugin =>
                        {
                            return -1 != plugin.Name.IndexOf(FilterCriteria, StringComparison.CurrentCultureIgnoreCase);
                        }).ToList();
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
                    : this._remotePlugins.Where(plugin =>
                        {
                            return -1 != plugin.Name.IndexOf(FilterCriteria, StringComparison.CurrentCultureIgnoreCase);
                        }).ToList();
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
                return RemotePlugins.Except(InstalledPlugins, new PluginComparer()).ToList();
            }
            set => throw new NotImplementedException();
        }

        public List<OutdatedPlugin> OutdatedPlugins
        {
            get
            {
                Dictionary<PluginDescription, PluginDescription> outdated = new Dictionary<PluginDescription, PluginDescription>();
                InstalledPlugins.ForEach(installedPlugin =>
                {
                    PluginDescription remotePlugin = RemotePlugins.Find(rp => isSamePlugin(rp, installedPlugin) && isNewPluginVersion(rp, installedPlugin));
                    if (null != remotePlugin)
                        outdated.Add(installedPlugin, remotePlugin);
                });

                return outdated.Select(PluginKvp =>
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
            LocalInstallerService.UninstallPlugin(plugin);
            RemotePluginRepository.GetAvailablePlugins();
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

            RemotePluginRepository = new S3PluginRepositoryService();
            LocalInstallerService = new LocalInstallerService(installPath);
            RemotePlugins = RemotePluginRepository.GetAvailablePlugins(onlyLatestPlugins);
            RefreshInstalled();
        }

        public void RefreshInstalled()
        {
            InstalledPlugins = LocalInstallerService.GetInstalledPlugins();
        }

        public static Func<PluginDescription, PluginDescription, bool> isSamePlugin = (PluginDescription x, PluginDescription y) => x.Name == y.Name && x.ShortName == y.ShortName;
        public static Func<PluginDescription, PluginDescription, bool> isNewPluginVersion = (PluginDescription x, PluginDescription y) => new Version(x.Version) > new Version(y.Version);
    }
}
