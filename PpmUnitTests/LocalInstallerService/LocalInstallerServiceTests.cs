using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.IO.Compression;
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
            //ILocalInstallerService localInstaller = new LocalInstallerService(System.IO.Path.GetTempPath());
            //List<PluginDescription> plugins = localInstaller.GetInstalledPlugins();
            //Assert.IsTrue("Translation Validation Plugin".Equals(plugins[0].Name));
        }

        [TestMethod()]
        public void InstallPluginTest()
        {
            // Set up names and text
            string shortName = "ipt";
            string json = $"{{'shortName': '{shortName}'}}";
            string name = "installTest";
            string text = $"{name} file text";
            string directory = name;
            string filename = $"{name}.txt";
            string archiveName = $"{name}.zip";
            string jsonName = $"{name}.json";

            /// Set up paths
            string basePath = System.IO.Path.GetTempPath();
            string installedPath = Path.Combine(basePath, shortName.ToUpper());
            string directoryPath = Path.Combine(basePath, directory);
            string filePath = Path.Combine(directoryPath, filename);
            string archivePath = Path.Combine(basePath, archiveName);
            string jsonPath = Path.Combine(basePath, jsonName);

            /// Delete the assets if they already exist
            if (File.Exists(filePath))
                File.Delete(filePath);
            if (File.Exists(archivePath))
                File.Delete(archivePath);
            if (File.Exists(jsonPath))
                File.Delete(jsonPath);

            /// Create the test assets
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            File.WriteAllText(filePath, text);
            ZipFile.CreateFromDirectory(directoryPath, archivePath);
            File.WriteAllText(jsonPath, json);

            /// Run the installer
            ILocalInstallerService localInstaller = new LocalInstallerService(basePath);
            localInstaller.InstallPlugin(new FileInfo(archivePath));

            /// Assert expectations
            Assert.IsTrue(Directory.Exists(installedPath));
            Assert.IsTrue(File.Exists(Path.Combine(installedPath, jsonName)));
            Assert.IsTrue(File.Exists(Path.Combine(installedPath, filename)));
            Assert.IsFalse(File.Exists(Path.Combine(basePath, jsonName)));
            Assert.IsFalse(File.Exists(Path.Combine(basePath, archiveName)));

            /// Cleanup
            Directory.Delete(installedPath, true);
            File.Delete(filePath);
            File.Delete(archivePath);
        }

        [TestMethod()]
        public void UninstallPluginTest()
        {
            /// Set up data
            PluginDescription plugin = new PluginDescription()
            {
                Name = "Uninstall Plugin Test",
                ShortName = "upt"
            };
            string basePath = System.IO.Path.GetTempPath();
            string directoryPath = Path.Combine(basePath, plugin.ShortName.ToUpper());
            
            /// Create assets
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            File.WriteAllText(Path.Combine(directoryPath, "file.txt"), "text");

            /// Run the uninstaller
            ILocalInstallerService localInstaller = new LocalInstallerService(System.IO.Path.GetTempPath());
            localInstaller.UninstallPlugin(plugin);

            /// Assert expectations
            Assert.IsFalse(Directory.Exists(directoryPath));
        }
    }
}