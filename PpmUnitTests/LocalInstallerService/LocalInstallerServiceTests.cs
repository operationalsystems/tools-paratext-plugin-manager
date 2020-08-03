using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using PpmMain.LocalInstaller;
using PpmMain.Models;

namespace PpmUnitTests
{
    [TestClass()]
    public class LocalInstallerServiceTests
    {
        [TestMethod()]
        public void GetInstalledPluginsTest()
        {
            ILocalInstallerService localInstaller = new LocalInstallerService();
            List<PluginDescription> plugins = localInstaller.GetInstalledPlugins();
            Assert.IsTrue("Translation Validation Plugin".Equals(plugins[0].Name)); /// this is dependent on the filesystem and needs to be replaced with a test that validates the logic appropriately
        }

        [TestMethod()]
        public void InstallPluginTest()
        {
            ILocalInstallerService localInstaller = new LocalInstallerService();
            localInstaller.InstallPlugin(new System.IO.FileInfo(@"C:\Program Files (x86)\Paratext 8\plugins\ParatextPluginManagerPlugin\download\TranslationValidationPlugin-1.2.0.0.zip")); /// for now, just invoke it so we can debug
        }
    }
}