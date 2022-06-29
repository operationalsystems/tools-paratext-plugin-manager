/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Microsoft.Win32;
using System;

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

        // Messages
        private const string ParatextNotInstalledMessage = "Paratext is not installed.";

        /// <summary>
        /// The installed Paratext version; If not installed, return null.
        /// </summary>
        public static string ParatextVersion
        {
            get
            {
                // grab and check the version from registry
                var paratextVersion = Registry.GetValue(ParatextRegistryBaseKey, ParatextVersionKey, null);
                _ = paratextVersion ?? throw new Exception(ParatextNotInstalledMessage);

                return (string)paratextVersion;
            }
        }

        /// <summary>
        /// The installed Paratext path; Throw an exception if Paratext not installed..
        /// </summary>
        public static string ParatextInstallPath
        {
            get
            {
                var paratextPath = Registry.GetValue(ParatextRegistryBaseKey, ParatextInstallPathKey, null);
                _ = paratextPath ?? throw new Exception(ParatextNotInstalledMessage);

                return (string)paratextPath;
            }
        }
    }
}