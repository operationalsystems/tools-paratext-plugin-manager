using Microsoft.VisualStudio.TestTools.UnitTesting;
using PpmMain.LocalInstallerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpmMain.LocalInstallerService.Tests
{
    [TestClass()]
    public class LocalInstallerServiceTests
    {
        [TestMethod()]
        public void GetInstalledPluginsTest()
        {
            List<PluginDescription> plugins = LocalInstallerService.GetInstalledPlugins();
            Assert.IsTrue("Some Plugin".Equals(plugins[0].name));
        }
    }
}