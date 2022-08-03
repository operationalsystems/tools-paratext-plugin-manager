using Microsoft.Extensions.Logging;
using Moq;
using PpmApp.PluginRepository;
using PpmApp.Util;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PpmUnitTests
{
    /// <summary>
    /// This is a helper class for setting up PluginRepositoryService for testing purposes.
    /// </summary>
    public class PluginRepositoryServiceTestsUtil
    {

        /// <summary>
        /// A Test specific temporary repo service download location.
        /// </summary>
        public static DirectoryInfo TestTemporaryDownloadLocation
        {
            get
            {
                return new DirectoryInfo(Path.Combine(Path.GetTempPath(), "PPMTEST"));
            }
        }


        public static Mock<PluginRepositoryService> SetupPluginRepositoryToDownloadTestPlugins(List<string> pluginFilenames)
        {
            // Mock: ILogger
            var _mockLogger = new Mock<ILogger<PluginRepositoryService>>();

            // Mock: PluginRepositoryService
            var mockPluginRepositoryService = new Mock<PluginRepositoryService>(_mockLogger.Object);
            mockPluginRepositoryService.Setup(pluginRepoService => pluginRepoService.TemporaryDownloadDirectory).Returns(TestTemporaryDownloadLocation);

            List<string> returnRepoPluginDescriptionJsonList = pluginFilenames.Select(pluginFilename =>
            {
                return $"{pluginFilename}.json";
            }).ToList();

            // set up expected return items.
            var returnDownloadedPluginDescriptionJsonList = returnRepoPluginDescriptionJsonList.Select(jsonFilename =>
            {
                return new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), $@"Resources\{jsonFilename}")).FullName;
            }).ToList();

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
                    for (int i = 0; i < returnDownloadedPluginDescriptionJsonList.Count; i++)
                    {
                        d.Add(returnDownloadedPluginDescriptionJsonList[i], Path.GetFileNameWithoutExtension(returnRepoPluginDescriptionJsonList[i]));
                    }
                    // set up the outputToInputMap
                })
                .Returns(returnDownloadedPluginDescriptionJsonList);

            return mockPluginRepositoryService;
        }
    }
}
