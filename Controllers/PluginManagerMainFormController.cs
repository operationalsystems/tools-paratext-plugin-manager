using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PpmMain.Models;
using PpmMain.LocalInstaller;
using PpmMain.PluginRepository;

namespace PpmMain.Controllers
{
    class PluginManagerMainFormController : IPluginManagerMainFormController
    {
        public List<PluginDescription> availablePlugins { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<PluginDescription> outdatedPlugins { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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

        public void UpdatePlugins(List<PluginDescription> plugins)
        {
            throw new NotImplementedException();
        }
    }
}
