using PpmMain.Models;
using System.Collections.Generic;
using System.IO;

namespace PpmMain.Util
{
    /// <summary>
    /// Handles management and installation of ParaText plugins.
    /// </summary>
    public interface IInstallerService
    {
        /// <summary>
        /// Gets descriptions of the currently installed ParaText plugins.
        /// </summary>
        /// <returns>The currently installed plugins.</returns>
        List<PluginDescription> GetInstalledPlugins();

        /// <summary>
        /// Installs a ParaText plugin.
        /// </summary>
        /// <param name="pluginArchive">The zip file containing the plugin data.</param>
        void InstallPlugin(FileInfo pluginArchive);

        /// <summary>
        /// Uninstalls a ParaText plugin.
        /// </summary>
        /// <param name="plugin">The plugin to uninstall.</param>
        void UninstallPlugin(PluginDescription plugin);
    }
}
