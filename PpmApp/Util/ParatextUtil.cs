/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PpmApp.Util
{
    /// <summary>
    /// Process-wide error utilities.
    /// </summary>
    public static class ParatextUtil
    {
        /// <summary>
        /// Maximum amount of time in milliseconds to wait for a Paratext process to exit.
        /// </summary>
        const int MAX_PT_PROCESS_WAIT_TIME_MS = 30 * 1000; // 30 seconds

        // Paratext Registry key information variables
        private const string ParatextRegistryRoot = "HKEY_LOCAL_MACHINE";
        private const string ParatextRegistryBaseKey = ParatextRegistryRoot + @"\SOFTWARE\Paratext\8";
        private const string ParatextVersionKey = "ParatextVersion";
        private const string ParatextInstallPathKey = "Paratext9_Full_Release_AppPath";
        private const string ParatextProcessName = "Paratext";

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

        /// <summary>
        /// The list of all running Paratext processes.
        /// </summary>
        private static Process[] ParatextProcesses
        {
            get {
                return Process.GetProcessesByName(ParatextProcessName);
            }
        }

        /// <summary>
        /// Whether Paratext is running or not
        /// </summary>
        public static Boolean IsParatextRunning
        {
            get
            {
                return (ParatextProcesses.Length > 1);
            }
        }

        /// <summary>
        /// Utility for killing all running Paratext processes
        /// </summary>
        public static void CloseParatext()
        {
            // initiate killing each Paratext process
            foreach (Process ptProcess in ParatextProcesses)
            {
                ptProcess.Kill();
            }

            // track if all the processes have successfully exited
            var processesExitSuccess = new bool[ParatextProcesses.Length]; // bool defaults as false

            // wait for each process to exit
            Parallel.For(0, ParatextProcesses.Length, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, proc =>
            {
                // wait for the process to exit for a maximum alotted time. And track if successfully exited
                processesExitSuccess[proc] = ParatextProcesses[proc].WaitForExit(MAX_PT_PROCESS_WAIT_TIME_MS);
            });

            var allSucceeded = Array.TrueForAll(processesExitSuccess, (procExitSuccess) => { return procExitSuccess; });

            if (!allSucceeded)
            {
                throw new Exception("Paratext was not closed successfully");
            }
        }
    }
}