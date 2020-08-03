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
        void InstallPlugin(PluginDescription plugin);
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
        public void InstallPlugin(PluginDescription plugin)
        {
            if (NeedsElevatedPermissions())
                ElevatePermissions();

            try
            {
                string downloadedPluginPath = Path.Combine(ppmPath, ppmDownloadedPluginsDirectory);
                string installedPluginDataPath = Path.Combine(ppmPath, ppmInstalledPluginDataDirectory);
                string pluginInstallPath = Path.Combine(ptInstallationPath, ptInstalledPluginsDirectory, plugin.ShortName.ToUpper());
                string zipFileName = Path.ChangeExtension(plugin.Filename, "zip");
                string zipFilePath = Path.Combine(downloadedPluginPath, zipFileName);
                /// If this is an upgrade, or a re-install, uninstall the plugin before extracting
                if (Directory.Exists(pluginInstallPath))
                    UninstallPlugin(plugin);
                ZipFile.ExtractToDirectory(zipFilePath, pluginInstallPath);
                File.Move(Path.Combine(downloadedPluginPath, plugin.Filename), Path.Combine(installedPluginDataPath, plugin.Filename));
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
                string[] pluginDescriptionFiles = Directory.GetFiles(directory, "*.json");
                foreach (string filePath in pluginDescriptionFiles)
                {
                    string rawPluginDescription = File.ReadAllText(filePath);
                    PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(rawPluginDescription);
                    pluginDescription.Filename = Path.GetFileName(filePath);
                    pluginDescriptions.Add(pluginDescription);
                }
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
    }
}
