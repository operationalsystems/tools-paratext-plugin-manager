/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Microsoft.Extensions.Logging;
using PpmApp.LocalInstaller;
using PpmApp.Models;
using PpmApp.PluginRepository;
using PpmApp.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PpmApp.Controllers
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
    public class PluginManagerMainFormController : IPluginManagerMainFormController
    {
        /// <summary>
        /// Type-specific logger (injected).
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Remote Plugin repository (injected).
        /// </summary>
        private readonly IPluginRepository _pluginRepoService;

        /// <summary>
        /// Remote Plugin repository (injected).
        /// </summary>
        private readonly IInstallerService _localInstallerService;

        public string FilterCriteria { get; set; }

        /// <summary>
        /// Simple constructor
        /// </summary>
        /// <param name="pluginRepoService">Plugin Repository Service.</param>
        public PluginManagerMainFormController(PluginRepositoryService pluginRepoService)
        {
            string installPath = Path.Combine(ParatextUtil.ParatextInstallPath, "plugins");
            var onlyLatestPlugins = true;
            var onlyCompatiblePlugins = true;

            _localInstallerService = new LocalInstallerService(installPath);
            _pluginRepoService = pluginRepoService ?? throw new ArgumentNullException(nameof(pluginRepoService));

            RemotePlugins = _pluginRepoService.GetAvailablePlugins(onlyLatestPlugins, onlyCompatiblePlugins);
            RefreshInstalled();
        }


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
            FileInfo downloadedPlugin = _pluginRepoService.DownloadPlugin(plugin);
            _localInstallerService.InstallPlugin(downloadedPlugin);
            RefreshInstalled();
        }

        public void InstallPlugin(OutdatedPlugin plugin)
        {
            PluginDescription pluginToInstall = new PluginDescription()
            {
                Name = plugin.Name,
                ShortName = plugin.ShortName,
                Version = plugin.Version,
                Description = plugin.Description,
                VersionDescription = plugin.VersionDescription,
                PtVersions = plugin.PtVersions,
                License = plugin.License,
            };

            InstallPlugin(pluginToInstall);
        }

        public void UninstallPlugin(PluginDescription plugin)
        {
            var onlyLatestPlugins = true;
            var onlyCompatiblePlugins = true;

            _localInstallerService.UninstallPlugin(plugin);
            _pluginRepoService.GetAvailablePlugins(onlyLatestPlugins, onlyCompatiblePlugins);
            RefreshInstalled();
        }

        public void UpdatePlugins(List<OutdatedPlugin> plugins)
        {
            plugins.ForEach(plugin => InstallPlugin(plugin));
        }

        /// <summary>
        /// This method handles updating the list of installed plugins.
        /// </summary>
        private void RefreshInstalled()
        {
            InstalledPlugins = _localInstallerService.GetInstalledPlugins();
        }
    }
}
