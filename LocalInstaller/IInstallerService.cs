using PpmMain.Models;
using System.Collections.Generic;
using System.IO;

namespace PpmMain.LocalInstaller
{
    /// <summary>
    /// This interface represents a service that handles management and installation of ParaText plugins.
    /// </summary>
    public interface IInstallerService
    {
        /// <summary>
        /// This function gets descriptions of the currently installed ParaText plugins.
        /// </summary>
        /// <returns>The currently installed plugins.</returns>
        List<PluginDescription> GetInstalledPlugins();

        /// <summary>
        /// This function installs a ParaText plugin.
        /// </summary>
        /// <param name="pluginArchive">The zip file containing the plugin data.</param>
        void InstallPlugin(FileInfo pluginArchive);

        /// <summary>
        /// This function uninstalls a ParaText plugin.
        /// </summary>
        /// <param name="plugin">The plugin to uninstall.</param>
        void UninstallPlugin(PluginDescription plugin);
    }
}
