using System.Collections.Generic;
using PpmMain.Models;

namespace PpmMain.Controllers
{
    interface IPluginManagerMainFormController
    {
        /// <summary>
        /// A list of plugins that are available to be installed.
        /// </summary>
        List<PluginDescription> availablePlugins { get; set; }

        /// <summary>
        /// A list of plugins that have been installed and for which there are available updates.
        /// </summary>
        List<OutdatedPlugin> outdatedPlugins { get; set; }

        /// <summary>
        /// A list of plugins that have been installed.
        /// </summary>
        List<PluginDescription> installedPlugins { get; set; }

        /// <summary>
        /// A <c>string</c> that limits the plugins that are displayed in a list.
        /// </summary>
        string filterCriteria { get; set; }

        /// <summary>
        /// This method installs a given plugin.
        /// </summary>
        /// <param name="plugin">The plugin to install.</param>
        public void InstallPlugin(PluginDescription plugin);

        /// <summary>
        /// This method uninstalls a given plugin.
        /// </summary>
        /// <param name="plugin">The plugin to uninstall.</param>
        public void UninstallPlugin(PluginDescription plugin);

        /// <summary>
        /// This method updates a given list of installed plugins.
        /// </summary>
        /// <param name="plugins">A list of installed plugins to update.</param>
        public void UpdatePlugins(List<OutdatedPlugin> plugins);
    }
}
