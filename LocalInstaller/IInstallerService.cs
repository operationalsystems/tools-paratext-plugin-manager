/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
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
