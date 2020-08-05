using System.Collections.Generic;

namespace PpmMain.Models
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
    }
}
