/*
Copyright © 2022 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
namespace PpmApp.Util
{
    public static class MainConsts
    {
        /// <summary>
        /// The name of the application.
        /// </summary>
        public const string ApplicationName = "Paratext Plugin Manager";

        /// <summary>
        /// The current application version.
        /// </summary>
        public const string ApplicationVersion = "1.3.1.0";

        /// <summary>
        /// The publisher of the application.
        /// </summary>
        public const string ApplicationPublisher = "Biblica, Inc.";

        /// <summary>
        /// A description of the application.
        /// </summary>
        public const string ApplicationDescription = "Application for performing Paratext Plugin installation and version management.";

        /// <summary>
        /// The copyright information for the application.
        /// </summary>
        public const string Copyright = "© 2020-2023 Biblica, Inc.";

        /// <summary>
        /// The directory name for downloaded plugins.
        /// </summary>
        public const string PluginDownloadDirectoryName = "PPM";

        /// <summary>
        /// The extension used by Plugin Manifest files
        /// </summary>
        public const string PluginManifestExtension = ".json";

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
        public const string PluginListChangedMessage = "Please start Paratext to see your changes";


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

        /// <summary>
        /// This is the URL to get support for the plugin.
        /// </summary>
        public const string SUPPORT_URL = "https://translationtools.biblica.com/en/support/home";
    }
}
