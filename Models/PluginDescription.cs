using System;
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

        /// <summary>
        /// DO NOT USE. Filename is being deprecated.
        /// </summary>
        public string Filename;

        public PluginDescription(string name, string shortName, string version, List<string> ptVersions, string license = null, string versionDescription = null, string description = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ShortName = shortName ?? throw new ArgumentNullException(nameof(shortName));
            Version = version ?? throw new ArgumentNullException(nameof(version));
            PtVersions = ptVersions ?? throw new ArgumentNullException(nameof(ptVersions));
            License = license;
            Description = description;
            VersionDescription = versionDescription;
        }


    }
}
