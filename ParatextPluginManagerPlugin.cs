/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using AddInSideViews;
using PpmPlugin.Util;
using System;
using System.AddIn;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PpmPlugin
{
    /// <summary>
    /// Paratext Plugin Manager plugin root class.
    /// </summary>
    [AddIn(MainConsts.PluginName, Description = MainConsts.PluginDescription, Version = MainConsts.PluginVersion, Publisher = MainConsts.PluginPublisher)]
    [QualificationData(PluginMetaDataKeys.menuText, MainConsts.PluginName)]
    [QualificationData(PluginMetaDataKeys.insertAfterMenuName, "Tools|")]
    [QualificationData(PluginMetaDataKeys.enableWhen, WhenToEnable.always)]
    [QualificationData(PluginMetaDataKeys.multipleInstances, CreateInstanceRule.always)]

    public class ParatextPluginManagerPlugin : IParatextAddIn2
    {

        /// <summary>
        /// No-op, to fulfill IParatextAddIn2 contract.
        /// </summary>
        public Dictionary<string, IPluginDataFileMergeInfo> DataFileKeySpecifications => null;

        /// <summary>
        /// No-op, to fulfill IParatextAddIn2 contract.
        ///
        /// Should never by invoked when CreateInstanceRule.always setting in place (above).
        /// </summary>
        /// <param name="activeProjectName">Active Paratext project name.</param>
        public void Activate(string activeProjectName)
        {
        }

        /// <summary>
        /// Called when plugin is requested to shut down.
        ///
        /// Terminates process, since plugins are standalone processes (not in-process libraries).
        /// </summary>
        public void RequestShutdown()
        {
            Environment.Exit(0);
        }

        /// <summary>
        /// Overridable utility method to show message boxes.
        /// </summary>
        /// <param name="messageText">Message box text (required).</param>
        /// <param name="messageButtons">Message box buttons (required).</param>
        /// <param name="messageIcon">Message box icon (required).</param>
        /// <returns>Result from message box call (e.g., "Cancel").</returns>
        public virtual DialogResult ShowMessageBox(string messageText, MessageBoxButtons messageButtons, MessageBoxIcon messageIcon)
        {
            return MessageBox.Show(messageText, "Notice...", messageButtons, messageIcon);
        }

        /// <summary>
        /// Entry point method.
        /// </summary>
        /// <param name="host">Host interface, providing access to Paratext services.</param>
        /// <param name="activeProjectName">Active Paratext project name.</param>
        public void Run(IHost host, string activeProjectName)
        {
            lock (this)
            {
                // track host & plugin reference for static error utilities
                HostUtil.Instance.Host = host;
                HostUtil.Instance.TypesettingPreviewPlugin = this;

                try
                {
                    // Create main thread & delegate
                    Application.EnableVisualStyles();
                    var uiThread = new Thread(() =>
                    {
#if DEBUG
                        // Provided because plugins are separate processes that may only be attached to,
                        // once instantiated (can't run Paratext and automatically attach, as with shared libraries).
                        ShowMessageBox($"Attach debugger now to PID {Process.GetCurrentProcess().Id}, if needed!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif

                        try
                        {
                            MessageBox.Show(@$"Application name: '{host.ApplicationName}', version: '{host.ApplicationVersion}') has been installed.");

                        }
                        catch (Exception ex)
                        {
                            // Variables for tracking error information.
                            var errorDetails = new Dictionary<string, string>();
                            string message = ex.Message;

                            // Report the error
                            ReportErrorWithDetails(message, errorDetails);
                        }
                        finally
                        {
                            // Exit process (terminate plugin) once complete, no matter what.
                            Environment.Exit(0);
                        }
                    })
                    { IsBackground = false };

                    // Execute main thread.
                    uiThread.SetApartmentState(ApartmentState.STA);
                    uiThread.Start();
                }
                catch (Exception ex)
                {
                    // Log any errors that make it this far and re-throw to give Paratext a heads-up.
                    HostUtil.Instance.ReportError(null, ex);
                    throw;
                }
            }
        }

        /// <summary>
        /// Function for normalizing how we print errors.
        /// </summary>
        /// <param name="message">The error message. (required)</param>
        /// <param name="details">The error details. (optional)</param>
        /// <param name="printException">Whether to print the exception or not. True: print the exception; False: don't print the exception. Default: false</param>
        /// <param name="ex">The error's associated exception. (required if <c>printException</c> is <c>true</c>)</param>
        public static void ReportErrorWithDetails(
            string message,
            IDictionary<string, string> details = null,
            bool printException = false,
            Exception ex = null
            )
        {
            // validate required inputs
            _ = message ?? throw new ArgumentNullException(nameof(message));
            if (printException)
            {
                _ = ex ?? throw new ArgumentNullException(nameof(ex));
            }

            // initialize string builder with error message
            StringBuilder msgSb = new StringBuilder($"{message}\r\n");

            // add the details of the error the message string builder
            if (details != null)
            {
                foreach (KeyValuePair<string, string> item in details)
                {
                    msgSb.AppendLine($"    {item.Key}: {item.Value}");
                }
            }

            // report the prettified error
            if (printException)
            {
                HostUtil.Instance.ReportError(msgSb.ToString(), ex);
            }
            else
            {
                HostUtil.Instance.ReportError(msgSb.ToString(), null);
            }
        }
    }
}
