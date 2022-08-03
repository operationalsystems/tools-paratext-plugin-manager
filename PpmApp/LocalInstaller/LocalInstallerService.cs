/*
Copyright © 2022 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Newtonsoft.Json;
using PpmApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace PpmApp.LocalInstaller
{
    /// <summary>
    /// This service handles management and installation of local ParaText plugins.
    /// </summary>
    public class LocalInstallerService : IInstallerService
    {
        /// <summary>
        /// The directory name where ParaText plugins are installed.
        /// </summary>
        private string PtInstalledPluginsPath { get; set; }

        /// <summary>
        /// The directory name where 3rd party installers are put while we execute them.
        /// </summary>
        private string TempExecutablePath { get; } = Path.Combine(Path.GetTempPath(), "PpmTempExecutableDirectory");

        public LocalInstallerService(string ptInstalledPluginsPath)
        {
            PtInstalledPluginsPath = ptInstalledPluginsPath ?? throw new ArgumentNullException(nameof(ptInstalledPluginsPath));
        }

        /// <summary>
        /// This function gets descriptions of the currently installed ParaText plugins.
        /// </summary>
        /// <returns>The currently installed plugins.</returns>
        public List<PluginDescription> GetInstalledPlugins()
        {
            List<PluginDescription> pluginDescriptions = new List<PluginDescription>();

            string[] pluginDirectoryPaths = Directory.GetDirectories(PtInstalledPluginsPath);
            foreach (string directoryPath in pluginDirectoryPaths)
            {
                string[] pluginDescriptionFilePaths = Directory.GetFiles(directoryPath, "*.json");
                foreach (string filePath in pluginDescriptionFilePaths)
                {
                    PluginDescription plugin = GetPluginDescription(filePath);
                    _ = plugin.Name ?? throw new ArgumentNullException(nameof(plugin.Name));
                    _ = plugin.ShortName ?? throw new ArgumentNullException(nameof(plugin.ShortName));
                    if (plugin.PtVersions is null || plugin.PtVersions.Count() == 0)
                        throw new ArgumentNullException(nameof(plugin.PtVersions));

                    pluginDescriptions.Add(plugin);
                }

            }

            return pluginDescriptions;
        }

        /// <summary>
        /// This function installs a ParaText plugin.
        /// </summary>
        /// <param name="plugin">The plugin to uninstall.</param>
        /// <param name="pluginArchive">The zip file containing the plugin data.</param>
        public void InstallPlugin(PluginDescription plugin, FileInfo pluginArchive)
        {
            string zipFilePath = pluginArchive.FullName;
            string jsonFilePath = Path.ChangeExtension(zipFilePath, "json");
            string pluginDirectory = plugin.ShortName.ToUpper();
            string pluginInstallPath = Path.Combine(PtInstalledPluginsPath, pluginDirectory);
            /// If this is an upgrade, or a re-install, uninstall the plugin before extracting
            if (Directory.Exists(pluginInstallPath))
            {
                UninstallPlugin(plugin);
            }

            // create/recreate the directory, if removed
            if (!Directory.Exists(pluginInstallPath))
            {
                Directory.CreateDirectory(pluginInstallPath);
            }

            switch (plugin.InstallType)
            {
                case PluginInstallTypeEnum.Executable:
                    // clean temp plugin executable if still around
                    var tempExeInstallDir = new DirectoryInfo(Path.Combine(TempExecutablePath, plugin.ShortName));
                    if (tempExeInstallDir.Exists)
                    {
                        tempExeInstallDir.Delete(true);
                    }
                    // create the temp dir
                    tempExeInstallDir.Create();

                    // Extract executable to temp location
                    ZipFile.ExtractToDirectory(pluginArchive.FullName, tempExeInstallDir.FullName);
                    // Execute the installer

                    ProcessStartInfo processStartInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = false,
                        WorkingDirectory = tempExeInstallDir.FullName,
                        FileName = plugin.InstallExecutableFilename,
                        Arguments = plugin?.InstallExecutableArgs != null ? plugin.InstallExecutableArgs : ""
                    };

                    var exeProcess = Process.Start(processStartInfo);
                    exeProcess.WaitForExit();

                    // assess if successful or not
                    if (exeProcess.ExitCode != 0)
                    {
                        throw new Exception($"The plugin installer did not succeed. Exit Code: {exeProcess.ExitCode}");
                    }
                    break;
                default: // presume plugin install type. This supports legacy defaults if not specified
                    ZipFile.ExtractToDirectory(pluginArchive.FullName, pluginInstallPath);
                    break;
            }
            string targetJsonFilePath = Path.Combine(pluginInstallPath, Path.GetFileName(jsonFilePath));
            File.Move(jsonFilePath, targetJsonFilePath);

            // cleanup
            File.Delete(pluginArchive.FullName);
        }

        /// <summary>
        /// This function uninstalls a ParaText plugin.
        /// </summary>
        /// <param name="plugin">The plugin to uninstall.</param>
        public void UninstallPlugin(PluginDescription plugin)
        {
            string pluginInstallPath = Path.Combine(PtInstalledPluginsPath, plugin.ShortName.ToUpper());

            switch (plugin.InstallType)
            {
                case PluginInstallTypeEnum.Executable:
                    ProcessStartInfo processStartInfo = new ProcessStartInfo()
                    {
                        UseShellExecute = false,
                        WorkingDirectory = pluginInstallPath,
                        FileName = plugin.UninstallExecutableFilename,
                        Arguments = plugin?.UninstallExecutableArgs != null ? plugin.InstallExecutableArgs : ""
                    };

                    var exeProcess = Process.Start(processStartInfo);
                    exeProcess.WaitForExit();

                    // assess if successful or not
                    if (exeProcess.ExitCode != 0)
                    {
                        throw new Exception($"The plugin installer did not succeed. Exit Code: {exeProcess.ExitCode}");
                    }
                    break;
                default: // presume plugin install type. This supports legacy defaults if not specified
                    break;
            }

            if (Directory.Exists(pluginInstallPath))
            {
                Directory.Delete(pluginInstallPath, true);
            }
        }

        /// <summary>
        /// This function gets a plugin description.
        /// </summary>
        /// <param name="filePath">A file which describes the plugin.</param>
        /// <returns>A plugin description.</returns>
        public static PluginDescription GetPluginDescription(string filePath)
        {
            string rawPluginDescription = File.ReadAllText(filePath);
            PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(rawPluginDescription);

            return pluginDescription;
        }

        /// <summary>
        /// Determine if a plugin is installed.
        /// </summary>
        /// <param name="pluginDescription">Description of the plugin to check installation status of.</param>
        /// <returns>true: plugin is installed; false: plugin is not installed.</returns>
        public bool IsPluginInstalled(PluginDescription pluginDescription)
        {
            var installedPlugins = GetInstalledPlugins();
            var foundTargetPlugin = false;
            installedPlugins.ForEach(pluginDescription => {
                if (pluginDescription.ShortName.Equals(pluginDescription.ShortName))
                {
                    foundTargetPlugin = true;
                }
            });

            return foundTargetPlugin;
        }
    }
}
