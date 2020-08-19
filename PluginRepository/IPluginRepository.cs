using PpmMain.Models;
using System.Collections.Generic;
using System.IO;

namespace PpmMain.PluginRepository
{
    /// <summary>
    /// This Interface defines a service contract for a plugin repository.
    /// </summary>
    interface IPluginRepository
    {
        /// <summary>
        /// This function return a list of the available plugins.
        /// </summary>
        /// <param name="latestOnly">true: return only the latest available version of plugins; false: return all versions of plugins. default: true</param>
        /// <param name="compatibleOnly">true: return only the versions which are compatible with the current version of Paratext; false: return all versions of plugins. default: true</param>
        /// <returns>A list of available plugins.</returns>
        List<PluginDescription> GetAvailablePlugins(bool latestOnly = true, bool compatibleOnly = true);

        /// <summary>
        /// This function downloads a specified plugin.
        /// </summary>
        /// <param name="pluginShortname">The shortname of a plugin to download. EG: "TPT" for Typesetting Preview Tool. (required)</param>
        /// <param name="pluginVersion">The plugin's version to download. EG: "1.2.3.4". (required)</param>
        /// <param name="downloadDirectory">The directory where the plugin should be downloaded. (optional)</param>
        /// <returns>The file information of the downloaded file.</returns>
        FileInfo DownloadPlugin(string pluginShortname, string pluginVersion, DirectoryInfo downloadDirectory = null);

        /// <summary>
        /// This function downloads a specified plugin.
        /// </summary>
        /// <param name="pluginDescription">The description of the plugin to download. (required)</param>
        /// <param name="downloadDirectory">The directory where to download the plugin. (optional)</param>
        /// <returns>The file information of the downloaded file.</returns>
        FileInfo DownloadPlugin(PluginDescription pluginDescription, DirectoryInfo downloadDirectory = null);
    }
}
