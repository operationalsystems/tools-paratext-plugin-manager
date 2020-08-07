using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpmMain.Models
{
    public class OutdatedPlugin : PluginDescription
    {
        /// <summary>
        /// The currently installed version of the plugin.
        /// </summary>
        public string InstalledVersion { get; set; }
    }
}
