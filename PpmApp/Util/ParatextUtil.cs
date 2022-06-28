/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace PpmApp.Util
{
    /// <summary>
    /// Process-wide error utilities.
    /// </summary>
    public static class ParatextUtil
    {
        // Paratext Registry key information variables
        private const string ParatextRegistryRoot = "HKEY_LOCAL_MACHINE";
        private const string ParatextRegistryBaseKey = ParatextRegistryRoot + @"\SOFTWARE\Paratext\8";
        private const string ParatextVersionKey = "ParatextVersion";
        private const string ParatextInstallPathKey = "Paratext9_Full_Release_AppPath";

        /// <summary>
        /// The installed Paratext version; If not installed, return null.
        /// </summary>
        public static string ParatextVersion
        {
            get
            {
                return (string)Registry.GetValue(ParatextRegistryBaseKey, ParatextVersionKey, null);
            }
        }

        /// <summary>
        /// The installed Paratext path; If not installed, return null.
        /// </summary>
        public static string ParatextInstallPath
        {
            get
            {
                return (string)Registry.GetValue(ParatextRegistryBaseKey, ParatextInstallPathKey, null);
            }
        }

        /// <summary>
        /// Reports exception to log and message box w/prefix text.
        ///
        /// Either prefixText (or) ex must be non-null.
        /// </summary>
        /// <param name="prefixText">Prefix text (optional, may be null; default used when null).</param>
        /// <param name="ex">Exception (optional, may be null).</param>
        public static void ReportError(string prefixText, Exception ex)
        {
            if (prefixText == null && ex == null)
            {
                throw new ArgumentNullException("prefixText (or) ex must be non-null");
            }

            var messageText = (prefixText ?? "Error: Please contact support.")
                + (ex == null ? string.Empty
                    : Environment.NewLine + Environment.NewLine
                    + "Details: " + ex + Environment.NewLine);

            MessageBox.Show(messageText, "Notice...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            LogLine($"Error: {messageText}", true);
        }

        /// <summary>
        /// Log text to Paratext's app log and the console.
        /// </summary>
        /// <param name="inputText">Input text (required).</param>
        /// <param name="isError">Error flag.</param>
        public static void LogLine(string inputText, bool isError)
        {
            (isError ? Console.Error : Console.Out).WriteLine(inputText);
            // TODO replace with event logger
        }
    }
}