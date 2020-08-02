using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.Win32;

namespace PpmMain.LocalInstallerService
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
            this.ptInstallationPath = GetPTInstallationPath();
            this.ppmPath = $"{ptInstallationPath}\\{ptInstalledPluginsDirectory}\\{ppmDirectory}";
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

        }
        public void UninstallPlugin(PluginDescription plugin)
        {

        }
        private bool NeedsElevatedPermissions()
        {
            return false;
        }
        private void ElevatePermissions()
        {
            ProcessStartInfo info = new ProcessStartInfo(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            info.Verb = "runas";
            Process.Start(info);
            System.Diagnostics.Process.Start(info);
        }
        public static List<PluginDescription> GetPluginDescriptions(string directory)
        {
            List<PluginDescription> pluginDescriptions = new List<PluginDescription>();

            try
            {
                string[] pluginDescriptionFiles = Directory.GetFiles(directory, "*.json");
                IEnumerable<string> rawPluginDescriptions = pluginDescriptionFiles.Select(filePath => System.IO.File.ReadAllText(filePath));
                foreach (string description in rawPluginDescriptions)
                    pluginDescriptions.Add(JsonConvert.DeserializeObject<PluginDescription>(description));
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
            char[] toTrim = {'\\'};
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

    /// <summary>
    /// The description of an installed plugin
    /// </summary>
    public class PluginDescription
    {
        public readonly string name;
        public readonly string shortName;
        public readonly string version;
        public readonly string description;
        public readonly string versionDescription;
        public readonly List<string> ptVersions;
        public readonly string license;

        public PluginDescription(string name, string shortName, string version, string description, string versionDescription, List<string> ptVersions, string license)
        {
            this.name = name;
            this.shortName = shortName;
            this.version = version;
            this.description = description;
            this.versionDescription = versionDescription;
            this.ptVersions = ptVersions;
            this.license = license;
        }


    }
    /// <summary>
    /// Interface for a service that fetches the data we need from the registry
    /// </summary>
    interface IRegistryService
    {
        RegistryKey ReadLocalMachineSubKey(string subKey);

        object ReadKeyValue(RegistryKey key, string name);
    }
    /// <summary>
    /// Fetches data from the registry
    /// </summary>
    public class RegistryService: IRegistryService
    {
        private readonly RegistryKey localMachine;

        public RegistryService()
        {
            this.localMachine = Registry.LocalMachine;
        }
        public RegistryKey ReadLocalMachineSubKey(string subKey)
        {
            return localMachine.OpenSubKey(subKey);
        }

        public object ReadKeyValue(RegistryKey key, string name)
        {
            return key.GetValue(name);
        }
    }
}
