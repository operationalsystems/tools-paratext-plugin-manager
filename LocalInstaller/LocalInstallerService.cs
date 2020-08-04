using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Newtonsoft.Json;
using PpmMain.Models;

namespace PpmMain.LocalInstaller
{
    public interface ILocalInstallerService
    {
        List<PluginDescription> GetInstalledPlugins();
        void InstallPlugin(FileInfo pluginArchive);
        void UninstallPlugin(PluginDescription plugin);
    }

    /// <summary>
    /// Handles management and installation of local plugins.
    /// </summary>
    public class LocalInstallerService : ILocalInstallerService
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
        /// Gets descriptions of the currently installed plugins.
        /// </summary>
        /// <returns></returns>
        public List<PluginDescription> GetInstalledPlugins()
        {
            return GetPluginDescriptions(PtInstalledPluginsPath);
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
        /// Gets plugin descriptions in a directory.
        /// </summary>
        /// <param name="directory">The directory to search for plugin descriptions.</param>
        /// <returns>A list of plugin descriptions.</returns>
        public static List<PluginDescription> GetPluginDescriptions(string directory)
        {
            List<PluginDescription> pluginDescriptions = new List<PluginDescription>();

            string[] pluginDescriptionFilePaths = Directory.GetFiles(directory, "*.json");
            foreach (string filePath in pluginDescriptionFilePaths)
                pluginDescriptions.Add(GetPluginDescription(filePath));

            return pluginDescriptions;
        }

        public static PluginDescription GetPluginDescription(string filePath)
        {
            string rawPluginDescription = File.ReadAllText(filePath);
            PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(rawPluginDescription);

            return pluginDescription;
        }

        public static PluginDescription GetPluginDescription(string filePath)
        {
            string rawPluginDescription = File.ReadAllText(filePath);
            PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(rawPluginDescription);

            return pluginDescription;
        }
    }
}
