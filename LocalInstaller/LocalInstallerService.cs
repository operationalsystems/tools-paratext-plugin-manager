using Newtonsoft.Json;
using PpmMain.Models;
using PpmMain.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace PpmMain.LocalInstaller
{
    /// <summary>
    /// Handles management and installation of local ParaText plugins.
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
        /// Gets descriptions of the currently installed ParaText plugins.
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
        /// Installs a ParaText plugin.
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
        /// Uninstalls a ParaText plugin.
        /// </summary>
        /// <param name="plugin">The plugin to uninstall.</param>
        public void UninstallPlugin(PluginDescription plugin)
        {
            string pluginInstallPath = Path.Combine(PtInstalledPluginsPath, plugin.ShortName.ToUpper());
            if (Directory.Exists(pluginInstallPath))
                Directory.Delete(pluginInstallPath, true);
        }

        /// <summary>
        /// Gets a plugin description.
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
