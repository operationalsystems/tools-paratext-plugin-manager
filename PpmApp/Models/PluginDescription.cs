/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Newtonsoft.Json;
using System.Collections.Generic;

namespace PpmApp.Models
{
    /// <summary>
    /// The description of an installed plugin
    /// </summary>
    public class PluginDescription
    {
        /// <summary>
        /// The plugin name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique shortname representing the plugin.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// The version of the plugin.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// A description of the plugin.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A description of what has changed in this version of the plugin.
        /// </summary>
        public string VersionDescription { get; set; }

        /// <summary>
        /// The versions of ParaText that this plugin version is compatible with.
        /// </summary>
        public List<string> PtVersions { get; set; }

        /// <summary>
        /// The license for this plugin.
        /// </summary>
        public string License { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
