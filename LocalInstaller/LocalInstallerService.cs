using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Win32;
using System.Diagnostics;
using PpmMain.Models;
using PpmMain.Util;

namespace PpmMain.LocalInstaller
{
    public interface ILocalInstallerService
    {
        List<PluginDescription> GetInstalledPlugins();
        void InstallPlugin(FileInfo pluginArchive);
        void UninstallPlugin(PluginDescription plugin);
    }

    /// <summary>
    /// Handles management and installation of local plugins
    /// </summary>
    public class LocalInstallerService : ILocalInstallerService
    {
        private readonly string ptInstalledPluginsDirectory = "plugins";
        private readonly string ppmDirectory = "ParatextPluginManagerPlugin";
        private readonly string ppmInstalledPluginDataDirectory = "plugins";
        private readonly string ppmDownloadedPluginsDirectory = "downloads";
        private string ptInstallationPath;
        private string ppmPath;

        public LocalInstallerService()
        {
            ptInstallationPath = GetPTInstallationPath();
            ppmPath = $"{ptInstallationPath}\\{ptInstalledPluginsDirectory}\\{ppmDirectory}";
        }
        /// <summary>
        /// Gets descriptions of the currently installed plugins
        /// </summary>
        /// <returns></returns>
        public List<PluginDescription> GetInstalledPlugins()
        {
            return GetPluginDescriptions($"{ppmPath}\\{ppmInstalledPluginDataDirectory}");
        }
        /// <summary>
        /// Installs a ParaText plugin
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="pluginArchive"></param>
        public void InstallPlugin(FileInfo pluginArchive)
        {
            if (NeedsElevatedPermissions())
                ElevatePermissions();

            try
            {
                string zipFilePath = pluginArchive.FullName;
                string jsonFilePath = Path.ChangeExtension(zipFilePath, "json");
                PluginDescription plugin = GetPluginDescription(jsonFilePath);
                string pluginDirectory = plugin.ShortName;
                string pluginInstallPath = Path.Combine(ptInstallationPath, ptInstalledPluginsDirectory, pluginDirectory);
                /// If this is an upgrade, or a re-install, uninstall the plugin before extracting
                if (Directory.Exists(pluginInstallPath))
                    UninstallPlugin(plugin);
                ZipFile.ExtractToDirectory(pluginArchive.FullName, pluginInstallPath);
                File.Move(jsonFilePath, pluginInstallPath);
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }
        }
        public void UninstallPlugin(PluginDescription plugin)
        {
            if (NeedsElevatedPermissions())
                ElevatePermissions();

            try
            {
                string pluginInstallPath = $"{ptInstallationPath}\\{ptInstalledPluginsDirectory}\\{plugin.ShortName.ToUpper()}";
                if (Directory.Exists(pluginInstallPath))
                    Directory.Delete(pluginInstallPath);
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }
        }
        private bool NeedsElevatedPermissions()
        {
            return false;
        }
        private void ElevatePermissions()
        {
            ProcessStartInfo info = new ProcessStartInfo(Process.GetCurrentProcess().MainModule.FileName);
            info.Verb = "runas";
            Process.Start(info);
            Process.Start(info);
        }
        public static List<PluginDescription> GetPluginDescriptions(string directory)
        {
            List<PluginDescription> pluginDescriptions = new List<PluginDescription>();

            try
            {
                string[] pluginDescriptionFilePaths = Directory.GetFiles(directory, "*.json");
                foreach (string filePath in pluginDescriptionFilePaths)
                    pluginDescriptions.Add(GetPluginDescription(filePath));
            }
            catch (Exception ex)
            {
                ReportException(ex);
            }

            return pluginDescriptions;
        }
        /// <summary>
        /// Returns the ParaText installation directory
        /// </summary>
        /// <returns>The directory where ParaText is installed</returns>
        public static string GetPTInstallationPath()
        {
            string sixtyFourBitPath = "WOW6432Node\\";
            string ptVersion = "8";
            string subKey = $"SOFTWARE\\{sixtyFourBitPath}Paratext\\{ptVersion}";
            string name = $"Program_Files_Directory_Ptw{ptVersion}";

            RegistryService registryService = new RegistryService();
            RegistryKey key = registryService.ReadLocalMachineSubKey(subKey);
            char[] toTrim = { '\\' };
            return registryService.ReadKeyValue(key, name).ToString().TrimEnd(toTrim);
        }

        public static void ReportException(Exception ex)
        {
            // Variables for tracking error information.
            IDictionary<string, string> errorDetails = new Dictionary<string, string>();
            string message = ex.Message;

            // Report the error
            /// PpmMain.ParatextPluginManagerPlugin.ReportErrorWithDetails(message, errorDetails);
        }

        public static PluginDescription GetPluginDescription(string filePath)
        {
            string rawPluginDescription = File.ReadAllText(filePath);
            PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(rawPluginDescription);

            return pluginDescription;
        }
    }
}
