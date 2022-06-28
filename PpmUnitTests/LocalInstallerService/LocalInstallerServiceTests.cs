/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PpmApp.LocalInstaller;
using PpmApp.Models;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace PpmUnitTests
{
    [TestClass()]
    public class LocalInstallerServiceTests
    {
        [TestMethod()]
        public void GetInstalledPluginsTest()
        {
            /// Set up data
            string name = "gipt";
            string shortName1 = $"{name}1";
            string shortName2 = $"{name}2";
            string json1 = $@"{{'name': 'Get Installed Plugins Test 1','shortName': '{shortName1}','ptVersions': ['8','9']}}";
            string json2 = $@"{{'name': 'Get Installed Plugins Test 1','shortName': '{shortName2}','ptVersions': ['8','9']}}";
            string basePath = System.IO.Path.GetTempPath();
            string testDirectoryPath = Path.Combine(basePath, name);
            string directoryPath1 = Path.Combine(testDirectoryPath, shortName1.ToUpper());
            string directoryPath2 = Path.Combine(testDirectoryPath, shortName2.ToUpper());

            /// Create assets
            if (!Directory.Exists(testDirectoryPath))
                Directory.CreateDirectory(testDirectoryPath);
            if (!Directory.Exists(directoryPath1))
                Directory.CreateDirectory(directoryPath1);
            File.WriteAllText(Path.Combine(directoryPath1, "file.json"), json1);
            if (!Directory.Exists(directoryPath2))
                Directory.CreateDirectory(directoryPath2);
            File.WriteAllText(Path.Combine(directoryPath2, "file.json"), json2);

            // Assert expectations
            IInstallerService localInstaller = new LocalInstallerService(testDirectoryPath);
            List<PluginDescription> plugins = localInstaller.GetInstalledPlugins();
            Assert.IsTrue(plugins.Exists(plugin => plugin.ShortName == shortName1));
            Assert.IsTrue(plugins.Exists(plugin => plugin.ShortName == shortName2));

            // Cleanup
            Directory.Delete(testDirectoryPath, true);
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
            IInstallerService localInstaller = new LocalInstallerService(basePath);
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
            IInstallerService localInstaller = new LocalInstallerService(System.IO.Path.GetTempPath());
            localInstaller.UninstallPlugin(plugin);

            /// Assert expectations
            Assert.IsFalse(Directory.Exists(directoryPath));
        }
    }
}