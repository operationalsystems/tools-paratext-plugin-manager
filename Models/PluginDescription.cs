using System;
using System.Collections.Generic;

namespace PpmMain.Models
{
    /// <summary>
    /// The description of an installed plugin
    /// </summary>
    public class PluginDescription
    {
        public readonly string name;
        public readonly string shortName;
        public readonly string version;
        public readonly string description;
        public readonly string versionDescription;
        public readonly List<string> ptVersions;
        public readonly string license;
        public string filename;

        public PluginDescription(string name, string shortName, string version, string description, string versionDescription, List<string> ptVersions, string license)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(shortName) || String.IsNullOrEmpty(version) || ptVersions.Count == 0)
                throw new ArgumentException("Expected 'name', 'shortName', 'version', and 'ptVersions' to not be null or empty.");

            this.name = name;
            this.shortName = shortName;
            this.version = version;
            this.description = description;
            this.versionDescription = versionDescription;
            this.ptVersions = ptVersions;
            this.license = license;
        }


    }
}
