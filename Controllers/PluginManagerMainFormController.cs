using System;
using System.Collections.Generic;
using System.Linq;
using PpmMain.Models;
using PpmMain.LocalInstaller;
using PpmMain.PluginRepository;

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

        public List<PluginDescription> availablePlugins { get; set; }
        public List<OutdatedPlugin> outdatedPlugins {
            get
            {
                var outdated = Enumerable.Intersect(installedPlugins, availablePlugins, new PluginComparer());

                return (List<OutdatedPlugin>)outdated.Select(plugin =>
                {
                    PluginDescription _availablePlugin = availablePlugins.Find(p => p.Name == plugin.Name && p.ShortName == plugin.ShortName);
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
        public List<PluginDescription> installedPlugins { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string filterCriteria { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void InstallPlugin(PluginDescription plugin)
        {
            throw new NotImplementedException();
        }

        public void UninstallPlugin(PluginDescription plugin)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlugins(List<OutdatedPlugin> plugins)
        {
            throw new NotImplementedException();
        }

        public PluginManagerMainFormController()
        {
            string installPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "DT468");
            var onlyLatestPlugins = true;

            RemotePluginRepository = new S3PluginRepositoryService();
            LocalInstallerService = new LocalInstallerService(installPath);
            availablePlugins = RemotePluginRepository.GetAvailablePlugins(onlyLatestPlugins);
            installedPlugins = LocalInstallerService.GetInstalledPlugins();
        }
    }
}
