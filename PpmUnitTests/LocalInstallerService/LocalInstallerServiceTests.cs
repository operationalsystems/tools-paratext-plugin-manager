using Microsoft.VisualStudio.TestTools.UnitTesting;
using PpmMain.LocalInstallerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace PpmMain.LocalInstallerService.Tests
{
    [TestClass()]
    public class LocalInstallerServiceTests
    {
        [TestMethod()]
        public void GetInstalledPluginsTest()
        {
            ILocalInstallerService localInstaller = new LocalInstallerService();
            List<PluginDescription> plugins = localInstaller.GetInstalledPlugins();
            Assert.IsTrue("Translation Validation Plugin".Equals(plugins[0].name)); /// this is dependent on the filesystem and needs to be replaced with a test that validates the logic appropriately
        }

        [TestMethod()]
        public void InstallPluginTest()
        {
            ILocalInstallerService localInstaller = new LocalInstallerService();
            PluginDescription plugin = new PluginDescription("Some Plugin", "sp", "1.2.3.4", null, null, new List<string>() { "8", "9" }, null);
            plugin.filename = "somePlugin-1.2.3.4.json";
            localInstaller.InstallPlugin(plugin); /// for now, just invoke it so we can debug
        }
    }
}