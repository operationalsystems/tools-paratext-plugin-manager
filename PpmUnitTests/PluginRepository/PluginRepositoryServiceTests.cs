using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PpmMain.PluginRepository;
using PpmMain.Util;
using System.Collections.Generic;
using System.IO;

namespace PpmUnitTests
{
    [TestClass]
    public class PluginRepositoryServiceTests
    {
        private const string TEST_PLUGIN_SHORTNAME = "temp";
        private const string TEST_PLUGIN_VERSION_1 = "1.0";
        private const string TEST_PLUGIN_VERSION_2 = "2.0";

        /// <summary>
        /// Mock <c>S3PluginRepositoryService</c> so that we can mock away the S3 functionality.
        /// </summary>
        private Mock<PluginRepositoryService> mockPluginRepositoryService;

        /// <summary>
        /// A Test specific temporary repo service download location.
        /// </summary>
        private readonly DirectoryInfo TestTemporaryDownloadLocation = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "PPMTEST"));

        /// <summary>
        /// Test setup.
        /// </summary>
        [TestInitialize]
        [DeploymentItem(@"Resources", "Resources")]
        public void TestSetup()
        {
            // Mock: S3PluginRepositoryService
            mockPluginRepositoryService = new Mock<PluginRepositoryService>();
            mockPluginRepositoryService.Setup(pluginRepoService => pluginRepoService.TemporaryDownloadDirectory).Returns(TestTemporaryDownloadLocation);

            // set up expected return items.
            var repoJsonFilename1 = $"testplugin-{TEST_PLUGIN_VERSION_1}.json";
            var repoJsonFilename2 = $"testplugin-{TEST_PLUGIN_VERSION_2}.json";
            var returnRepoPluginDescriptionJsonList = new List<string>() {
                repoJsonFilename1,
                repoJsonFilename2
            };
            var downloadedJsonFilename1 = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), $@"Resources\testplugin-{TEST_PLUGIN_VERSION_1}.json")).FullName;
            var downloadedJsonFilename2 = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), $@"Resources\testplugin-{TEST_PLUGIN_VERSION_2}.json")).FullName;
            var returnDownloadedPluginDescriptionJsonList = new List<string>() {
                downloadedJsonFilename1,
                downloadedJsonFilename2
            };

            /// set up service under test
            // Return a fake list of plugins.
            mockPluginRepositoryService
                .Setup(pluginRepoService => pluginRepoService.GetPluginFilesByExtension(MainConsts.PluginManifestExtension))
                .Returns(returnRepoPluginDescriptionJsonList);
            // Download the locally available test JSON files.
            mockPluginRepositoryService
                .Setup(pluginRepoService =>
                    pluginRepoService.DownloadFiles(
                        returnRepoPluginDescriptionJsonList,
                        It.IsAny<Dictionary<string, string>>()
                        )
                    )
                .Callback<List<string>, Dictionary<string, string>>((l, d) =>
                {
                    // set up the outputToInputMap
                    d.Add(downloadedJsonFilename1, Path.GetFileNameWithoutExtension(repoJsonFilename1));
                    d.Add(downloadedJsonFilename2, Path.GetFileNameWithoutExtension(repoJsonFilename2));
                })
                .Returns(returnDownloadedPluginDescriptionJsonList);

        }

        /// <summary>
        /// This function ensures that the DownloadPlugin function is called as expected. It doesn't actually download a file, as that's specific 
        /// to the S3 interaction. This does validate that it's called as expected.
        /// </summary>
        [TestMethod]
        public void TestDownloadPlugin()
        {
            // Test that the Download plugin works as expected
            // Download the locally available Zip files.
            mockPluginRepositoryService
                .Setup(pluginRepoService =>
                    pluginRepoService.DownloadFile($"testplugin-{TEST_PLUGIN_VERSION_1}.zip", It.IsAny<DirectoryInfo>())
                    );

            var downloadedFilepath = mockPluginRepositoryService.Object.DownloadPlugin(TEST_PLUGIN_SHORTNAME, TEST_PLUGIN_VERSION_1, TestTemporaryDownloadLocation);
        }

        /// <summary>
        /// This functions tests the GetAvailablePlugins method. It first tests that the get the latest version of the plugin works, 
        /// followed by testing the workflow that returns all versions of all plugins.
        /// </summary>
        [TestMethod]
        public void TestGetAvailablePlugins()
        {
            // Test that the Get All Available Plugins path works
            var allPlugins = mockPluginRepositoryService.Object.GetAvailablePlugins(false);

            // Both versions of the temp plugin should be returned.
            Assert.IsNotNull(allPlugins);
            Assert.AreEqual(2, allPlugins.Count);
            Assert.AreEqual(TEST_PLUGIN_SHORTNAME, allPlugins[0].ShortName);
            Assert.AreEqual(TEST_PLUGIN_VERSION_1, allPlugins[0].Version);
            Assert.AreEqual(TEST_PLUGIN_SHORTNAME, allPlugins[1].ShortName);
            Assert.AreEqual(TEST_PLUGIN_VERSION_2, allPlugins[1].Version);
        }
    }
}
