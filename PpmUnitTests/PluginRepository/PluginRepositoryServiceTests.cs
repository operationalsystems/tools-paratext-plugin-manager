using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using PpmMain.PluginRepository;

namespace PpmUnitTests
{
    [TestClass]
    public class PluginRepositoryServiceTests
    {
        PluginRepositoryService pluginRepositoryService;

        /// <summary>
        /// Test setup.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            pluginRepositoryService = new PluginRepositoryService();
        }

        [TestMethod]
        public void TestDownloadPlugin()
        {
            // FileInfo DownloadPlugin(string pluginName, string pluginVersion, DirectoryInfo downloadDirectory)
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TestGetAvailablePlugins(bool latestOnly = true)
        {
            // test that the latestOnly flag works as expected, and results in only the latest plugins are returned.
            var latestPlugins = pluginRepositoryService.GetAvailablePlugins(true);

            // Test that we're able to return all of the plugins when latestOnly is false.
        }

    }
}
