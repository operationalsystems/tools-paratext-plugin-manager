using PpmMain.Properties;
using System.Diagnostics;
using System.Reflection.Emit;

namespace PpmMain.Util
{
    public static class MainConsts
    {
        public const string PluginName = "Paratext Plugin Manager";
        public const string PluginVersion = "0.0.0.1";
        public const string PluginPublisher = "Biblica, Inc.";
        public const string PluginDescription = "Plugin for performing Paratext Plugin installation and version management.";
        public const string CopyrightText = "© 2020 Biblica, Inc.";

        public const string ProgressBarInstalling = "Installing ...";
        public const string ProgressBarUninstalling = "Uninstalling ...";
        public const string ProgressBarUpdating = "Updating ...";
        public const string PluginListChangedMessage = "Please restart Paratext for changes to take effect.";

        public const string LicenseFormAccept = "I Agree";
        public const string LicenseFormCancel = "Cancel";
        public const string LicenseFormDismiss = "Dismiss";
        public const string LicenseFormTitle = "End User License Agreement";
        public const string LicenseFormDescription = "Press Page Down to see the rest of the agreement.";
    }
}
