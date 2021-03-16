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
