using PpmMain.Models;
using System.Collections.Generic;

namespace PpmMain.Controllers
{
    /// <summary>
    /// This interface represents a controller that manages the state and interactions for the Plugin Manager Main Form.
    /// </summary>
    interface IPluginManagerMainFormController
    {
        /// <summary>
        /// A list of plugins that are available to be installed.
        /// </summary>
        List<PluginDescription> AvailablePlugins { get; set; }

        /// <summary>
        /// A list of plugins that have been installed and for which there are available updates.
        /// </summary>
        List<OutdatedPlugin> OutdatedPlugins { get; set; }

        /// <summary>
        /// A list of plugins that have been installed.
        /// </summary>
        List<PluginDescription> InstalledPlugins { get; set; }

        /// <summary>
        /// A <c>string</c> that limits the plugins that are displayed in a list.
        /// </summary>
        string FilterCriteria { get; set; }

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
