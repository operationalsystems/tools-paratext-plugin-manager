using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpmMain.PluginRepository
{
    interface IPluginRepository
    {
        /// <summary>
        /// This function return a list of the available plugins.
        /// </summary>
        /// <param name="latestOnly">true: return only the latest available version of plugins; false: return all versions of plugins.</param>
        /// <returns>A list of available plugins.</returns>
        List<String> GetAvailablePlugins(bool latestOnly = true);

        /// <summary>
        /// This function downloads a specified plugin.
        /// </summary>
        /// <param name="pluginName">The shortname of a plugin to download. EG: "TPT" for Typesetting Preview Tool.</param>
        /// <param name="pluginVersion">The plugin's version to download. EG: "1.2.3.4".</param>
        /// <param name="downloadDirectory">The directory where to download the plugin.</param>
        /// <returns>The file information of the downloaded file.</returns>
        FileInfo DownloadPlugin(string pluginName, string pluginVersion, DirectoryInfo downloadDirectory);
    }
}
