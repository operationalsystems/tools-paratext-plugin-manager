using PpmMain.Properties;
using System.Diagnostics;
using System.Reflection.Emit;

namespace PpmMain.Util
{
    public static class MainConsts
    {
        /// <summary>
        /// The name of the plugin.
        /// </summary>
        public const string PluginName = "Paratext Plugin Manager";

        /// <summary>
        /// The current plugin version.
        /// </summary>
        public const string PluginVersion = "1.0.0.0";

        /// <summary>
        /// The publisher of the plugin.
        /// </summary>
        public const string PluginPublisher = "Biblica, Inc.";

        /// <summary>
        /// A description of the plugin.
        /// </summary>
        public const string PluginDescription = "Plugin for performing Paratext Plugin installation and version management.";

        /// <summary>
        /// The copyright information for the plugin.
        /// </summary>
        public const string Copyright = "© 2020 Biblica, Inc.";


        /// <summary>
        /// The text to display when installing a plugin.
        /// </summary>
        public const string ProgressBarInstalling = "Installing ...";

        /// <summary>
        /// The text to display when uninstalling a plugin.
        /// </summary>
        public const string ProgressBarUninstalling = "Uninstalling ...";

        /// <summary>
        /// The text to display when updating a plugin.
        /// </summary>
        public const string ProgressBarUpdating = "Updating ...";

        /// <summary>
        /// The text to display when a plugin has been added, removed, or updated.
        /// </summary>
        public const string PluginListChangedMessage = "Please restart Paratext for changes to take effect.";


        /// <summary>
        /// The text for the button used to accept the EULA.
        /// </summary>
        public const string LicenseFormAccept = "I Agree";

        /// <summary>
        /// The text for the button used to not accept the EULA.
        /// </summary>
        public const string LicenseFormCancel = "Cancel";

        /// <summary>
        /// The text for the button used to dismiss the informational EULA form.
        /// </summary>
        public const string LicenseFormDismiss = "Dismiss";

        /// <summary>
        /// The text to display at the top of the EULA form.
        /// </summary>
        public const string LicenseFormTitle = "End User License Agreement";

        /// <summary>
        /// The text to display above the EULA.
        /// </summary>
        public const string LicenseFormDescription = "Press Page Down to see the rest of the agreement.";
    }
}
