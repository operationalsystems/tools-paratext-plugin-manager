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
        public bool Equals(PluginDescription x, PluginDescription y) => PluginManagerMainFormController.isSamePlugin(x, y);

        public int GetHashCode(PluginDescription obj) => obj.Name.GetHashCode();
    }
    class PluginManagerMainFormController : IPluginManagerMainFormController
    {
        IPluginRepository RemotePluginRepository { get; set; }

        IInstallerService LocalInstallerService { get; set; }

        List<PluginDescription> RemotePlugins { get; set; }

        public List<PluginDescription> AvailablePlugins { 
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
        public List<PluginDescription> InstalledPlugins { get; set; }
        public string FilterCriteria { get; set; }

        public void InstallPlugin(PluginDescription plugin)
        {
            System.IO.FileInfo downloadedPlugin = RemotePluginRepository.DownloadPlugin(plugin);
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

        public static Func<PluginDescription, PluginDescription, bool> isSamePlugin = (PluginDescription x, PluginDescription y) => x.Name == y.Name && x.ShortName == y.ShortName;
        public static Func<PluginDescription, PluginDescription, bool> isNewPluginVersion = (PluginDescription x, PluginDescription y) => new Version(x.Version) > new Version(y.Version);
    }
}
