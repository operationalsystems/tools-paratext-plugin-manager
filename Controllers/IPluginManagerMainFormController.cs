/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
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
