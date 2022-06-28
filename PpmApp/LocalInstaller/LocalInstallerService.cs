/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Newtonsoft.Json;
using PpmApp.Models;
using System;
using System.Collections.Generic;
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
        /// <param name="pluginArchive">The zip file containing the plugin data.</param>
        public void InstallPlugin(FileInfo pluginArchive)
        {
            string zipFilePath = pluginArchive.FullName;
            string jsonFilePath = Path.ChangeExtension(zipFilePath, "json");
            PluginDescription plugin = GetPluginDescription(jsonFilePath);
            string pluginDirectory = plugin.ShortName.ToUpper();
            string pluginInstallPath = Path.Combine(PtInstalledPluginsPath, pluginDirectory);
            /// If this is an upgrade, or a re-install, uninstall the plugin before extracting
            if (Directory.Exists(pluginInstallPath))
                UninstallPlugin(plugin);
            ZipFile.ExtractToDirectory(pluginArchive.FullName, pluginInstallPath);
            string targetJsonFilePath = Path.Combine(pluginInstallPath, Path.GetFileName(jsonFilePath));
            File.Move(jsonFilePath, targetJsonFilePath);
            File.Delete(pluginArchive.FullName);
        }

        /// <summary>
        /// This function uninstalls a ParaText plugin.
        /// </summary>
        /// <param name="plugin">The plugin to uninstall.</param>
        public void UninstallPlugin(PluginDescription plugin)
        {
            string pluginInstallPath = Path.Combine(PtInstalledPluginsPath, plugin.ShortName.ToUpper());
            if (Directory.Exists(pluginInstallPath))
                Directory.Delete(pluginInstallPath, true);
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
    }
}
