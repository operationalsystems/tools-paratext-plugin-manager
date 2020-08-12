using PpmMain.LocalInstaller;
using PpmMain.Models;
using PpmMain.PluginRepository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PpmMain.Controllers
{
    internal class PluginComparer : IEqualityComparer<PluginDescription>
    {
        public bool Equals(PluginDescription x, PluginDescription y) => x.Name == y.Name && x.ShortName == y.ShortName;

        public int GetHashCode(PluginDescription obj) => obj.GetHashCode();
    }
    class PluginManagerMainFormController : IPluginManagerMainFormController
    {
        IPluginRepository RemotePluginRepository { get; set; }

        IInstallerService LocalInstallerService { get; set; }

        List<PluginDescription> RemotePlugins { get; set; }

        public List<PluginDescription> AvailablePlugins { 
            get 
            {
                return (List<PluginDescription>)RemotePlugins.Except(InstalledPlugins, new PluginComparer()).ToList();
            }
            set => throw new NotImplementedException(); 
        }

        public List<OutdatedPlugin> OutdatedPlugins
        {
            get
            {
                var outdated = Enumerable.Intersect(InstalledPlugins, RemotePlugins, new PluginComparer());

                return (List<OutdatedPlugin>)outdated.Select(plugin =>
                {
                    PluginDescription _availablePlugin = RemotePlugins.Find(p => p.Name == plugin.Name && p.ShortName == plugin.ShortName);
                    return new OutdatedPlugin
                    {
                        Name = plugin.Name,
                        ShortName = plugin.ShortName,
                        InstalledVersion = plugin.Version,
                        Version = _availablePlugin.Version,
                        Description = _availablePlugin.Description,
                        VersionDescription = _availablePlugin.VersionDescription,
                        PtVersions = _availablePlugin.PtVersions,
                        License = _availablePlugin.License
                    };
                });
            }
            set => throw new NotImplementedException();
        }
        public List<PluginDescription> InstalledPlugins { get; set; }
        public string filterCriteria { get; set; }

        public void InstallPlugin(PluginDescription plugin)
        {
            System.IO.FileInfo downloadedPlugin = RemotePluginRepository.DownloadPlugin(plugin);
            LocalInstallerService.InstallPlugin(downloadedPlugin);
            RefreshInstalled();
        }

        public void UninstallPlugin(PluginDescription plugin)
        {
            LocalInstallerService.UninstallPlugin(plugin);
            RefreshInstalled();
        }

        public void UpdatePlugins(List<OutdatedPlugin> plugins)
        {

        }

        public PluginManagerMainFormController()
        {
            string installPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DT468");
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
    }
}
