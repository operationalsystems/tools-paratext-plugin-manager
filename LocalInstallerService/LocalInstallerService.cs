using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PpmMain.LocalInstallerService
{
    /// <summary>
    /// Handles management and installation of local plugins
    /// </summary>
    public class LocalInstallerService
    {
        /// <summary>
        /// Gets descriptions of the currently installed plugins
        /// </summary>
        /// <returns></returns>
        public static List<PluginDescription> GetInstalledPlugins()
        {
            PluginDescription somePlugin = new PluginDescription("Some Plugin", "sp", "1.2.3.4", "", "", new List<string> { "8", "9" }, "");

            return new List<PluginDescription> { somePlugin };
        }
    }

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

        public PluginDescription(string name, string shortName, string version, string description, string versionDescription, List<string> ptVersions, string license)
        {
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
