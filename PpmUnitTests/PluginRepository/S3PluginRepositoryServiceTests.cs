using Microsoft.VisualStudio.TestTools.UnitTesting;
using PpmMain.PluginRepository;
using System;
using System.IO;

namespace PpmUnitTests
{
    [TestClass]
    public class PluginRepositoryServiceTests
    {
        S3PluginRepositoryService pluginRepositoryService;

        /// <summary>
        /// Test setup.
        /// </summary>
        [TestInitialize]
        public void TestSetup()
        {
            pluginRepositoryService = new S3PluginRepositoryService();
        }

        [TestMethod]
        public void TestDownloadPlugin()
        {
            pluginRepositoryService.DownloadPlugin("TPT", "1.3.0.0", new DirectoryInfo(Directory.GetCurrentDirectory()));
        }

        [TestMethod]
        public void TestGetAvailablePlugins()
        {
            // test that the latestOnly flag works as expected, and results in only the latest plugins are returned.
            var latestPlugins = pluginRepositoryService.GetAvailablePlugins(true);

            foreach (var plugin in latestPlugins)
            {
                Console.WriteLine(plugin);
            }
            // Test that we're able to return all of the plugins when latestOnly is false.
        }

    }
}
