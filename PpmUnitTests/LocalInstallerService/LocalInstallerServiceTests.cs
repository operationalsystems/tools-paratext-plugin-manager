/*
Copyright © 2022 by Biblica, Inc.

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
        IInstallerService localInstaller;

        /// <summary>
        /// A Test specific temporary repo service download location.
        /// </summary>
        public static DirectoryInfo TestTemporaryDownloadLocation
        {
            get
            {
                return new DirectoryInfo(Path.Combine(Path.GetTempPath(), "PpmLocalInstallerTest"));
            }
        }

        /// <summary>
        /// Test setup.
        /// </summary>
        [TestInitialize]
        [DeploymentItem("Resources", "Resources")]
        public void TestSetup()
        {
            // delete if still around
            if (TestTemporaryDownloadLocation.Exists)
            {
                TestTemporaryDownloadLocation.Delete(true);
            }

            // create/recreate
            TestTemporaryDownloadLocation.Create();

            // set up local installer under test
            localInstaller = new LocalInstallerService(TestTemporaryDownloadLocation.FullName);
        }


        [TestMethod()]
        public void GetInstalledPluginsTest()
        {
            /// Set up data
            string name = "gipt";
            string shortName1 = $"{name}1";
            string shortName2 = $"{name}2";
            string json1 = $@"{{'name': 'Get Installed Plugins Test 1','shortName': '{shortName1}','ptVersions': ['8','9']}}";
            string json2 = $@"{{'name': 'Get Installed Plugins Test 1','shortName': '{shortName2}','ptVersions': ['8','9']}}";
            string basePath = TestTemporaryDownloadLocation.FullName;
            string directoryPath1 = Path.Combine(basePath, shortName1.ToUpper());
            string directoryPath2 = Path.Combine(basePath, shortName2.ToUpper());

            /// Create assets
            if (!Directory.Exists(directoryPath1))
                Directory.CreateDirectory(directoryPath1);
            File.WriteAllText(Path.Combine(directoryPath1, "file.json"), json1);
            if (!Directory.Exists(directoryPath2))
                Directory.CreateDirectory(directoryPath2);
            File.WriteAllText(Path.Combine(directoryPath2, "file.json"), json2);

            // Assert expectations
            List<PluginDescription> plugins = localInstaller.GetInstalledPlugins();
            Assert.IsTrue(plugins.Exists(plugin => plugin.ShortName == shortName1));
            Assert.IsTrue(plugins.Exists(plugin => plugin.ShortName == shortName2));
        }

        [TestMethod()]
        public void InstallZipPluginTest()
        {
            // Set up plugin to install
            var pluginDescription = new PluginDescription()
            {
                ShortName = "ipt",
                Name = "installTest",
            };

            string json = $"{{'shortName': '{pluginDescription.ShortName}'}}";
            string text = $"{pluginDescription.Name} file text";
            string directory = pluginDescription.Name;
            string filename = $"{pluginDescription.Name}.txt";
            string archiveName = $"{pluginDescription.Name}.zip";
            string jsonName = $"{pluginDescription.Name}.json";

            /// Set up paths
            string basePath = TestTemporaryDownloadLocation.FullName;
            string installedPath = Path.Combine(basePath, pluginDescription.ShortName.ToUpper());
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
            localInstaller.InstallPlugin(pluginDescription, new FileInfo(archivePath));

            /// Assert expectations
            Assert.IsTrue(Directory.Exists(installedPath));
            Assert.IsTrue(File.Exists(Path.Combine(installedPath, jsonName)));
            Assert.IsTrue(File.Exists(Path.Combine(installedPath, filename)));
            Assert.IsFalse(File.Exists(Path.Combine(basePath, jsonName)));
            Assert.IsFalse(File.Exists(Path.Combine(basePath, archiveName)));
        }

        [TestMethod()]
        public void InstallExePluginTest()
        {
            // load plugin description
            var exePluginFilename = "TestExePlugin-1.0";
            var exePluginJsonFilepath = Path.Combine(Directory.GetCurrentDirectory(), $@"Resources\{exePluginFilename}.json");
            var exePluginZipFilepath = Path.Combine(Directory.GetCurrentDirectory(), $@"Resources\{exePluginFilename}.zip");
            var exePluginDescription = LocalInstallerService.GetPluginDescription(exePluginJsonFilepath);

            // based on plugin install executable type, kick off 3rd party plugin install
            Assert.AreEqual(exePluginDescription.InstallType, PluginInstallTypeEnum.Executable);
            localInstaller.InstallPlugin(exePluginDescription, new FileInfo(exePluginZipFilepath));

            // the local installer logic will throw an exception for an invalid install execution

            // validate install success
            Assert.IsTrue(localInstaller.IsPluginInstalled(exePluginDescription), "Executable plugin is not installed.");

            // kick off uninstall
            localInstaller.UninstallPlugin(exePluginDescription);

            // validate uninstall success
            Assert.IsFalse(localInstaller.IsPluginInstalled(exePluginDescription), "Executable plugin has not been uninstalled.");
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
            string basePath = TestTemporaryDownloadLocation.FullName;
            string directoryPath = Path.Combine(basePath, plugin.ShortName.ToUpper());

            /// Create assets
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            File.WriteAllText(Path.Combine(directoryPath, "file.txt"), "text");

            /// Run the uninstaller
            localInstaller.UninstallPlugin(plugin);

            /// Assert expectations
            Assert.IsFalse(Directory.Exists(directoryPath));
        }
    }
}