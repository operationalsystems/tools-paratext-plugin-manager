/*
Copyright © 2021 by Biblica, Inc.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using Newtonsoft.Json;
using PpmMain.Models;
using PpmMain.Properties;
using PpmMain.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace PpmMain.PluginRepository
{
    /// <summary>
    /// This service implements an HTTP-based S3 repository for plugin management.
    /// </summary>
    public class PluginRepositoryService : IPluginRepository
    {
        /// <summary>
        /// The HTTPClient to be used HTTP operations.
        /// </summary>
        public virtual HttpClient HttpClient { get; private set; } = new HttpClient();

        /// <summary>
        /// The directory used by default to download files from the plugin repository.
        /// </summary>
        public virtual DirectoryInfo TemporaryDownloadDirectory { get; private set; } = new DirectoryInfo(Path.Combine(Path.GetTempPath(), MainConsts.PluginDownloadDirectoryName));

        /// <summary>
        /// The plugin description dictionary used to map the plugin descriptions to the associated filenames on the repository.
        /// </summary>
        public Dictionary<PluginDescription, string> PluginDescriptionStore { get; private set; }

        /// <summary>
        /// Base constructor.
        /// </summary>
        public PluginRepositoryService()
        {
            // Create the temporary directory if it doesn't exist.
            if (!TemporaryDownloadDirectory.Exists)
            {
                TemporaryDownloadDirectory.Create();
            }
        }

        public FileInfo DownloadPlugin(string pluginShortname, string pluginVersion, DirectoryInfo downloadDirectory = null)
        {
            // validate input
            if (String.IsNullOrEmpty(pluginShortname))
            {
                throw new ArgumentException($"'{nameof(pluginShortname)}' must be non-null and non-empty.");
            }
            if (String.IsNullOrEmpty(pluginVersion))
            {
                throw new ArgumentException($"'{nameof(pluginVersion)}' must be non-null and non-empty.");
            }

            // if no download directory is specified, use the default temporary directory
            if (downloadDirectory == null)
            {
                downloadDirectory = TemporaryDownloadDirectory;
            }

            // Download the available plugins if we haven't already
            if (PluginDescriptionStore == null)
            {
                GetAvailablePlugins();
            }

            // Grab the actual filename of the plugin, based on the plugin description's filename
            var results = PluginDescriptionStore
                .Select(i => i)
                .Where(d =>
                    d.Key.ShortName.Equals(pluginShortname, StringComparison.InvariantCultureIgnoreCase)
                    && d.Key.Version.Equals(pluginVersion, StringComparison.InvariantCultureIgnoreCase)
                );

            // make sure the return value isn't empty first
            if (results.Count() <= 0)
            {
                throw new ArgumentException($"Unable to find a plugin with '{nameof(pluginShortname)}' of '{pluginShortname}', and '{nameof(pluginVersion)}' of '{pluginVersion}'");
            }

            var result = results.First();

            return DownloadFile($"{result.Value}.zip");
        }

        public FileInfo DownloadPlugin(PluginDescription pluginDescription, DirectoryInfo downloadDirectory = null)
        {
            // validate input
            _ = pluginDescription ?? throw new ArgumentNullException(nameof(pluginDescription));

            return DownloadPlugin(pluginDescription.ShortName, pluginDescription.Version, downloadDirectory);
        }

        public List<PluginDescription> GetAvailablePlugins(bool latestOnly = true, bool compatibleOnly = true)
        {
#if DEBUG
            int currentPtVersion = 8;
#else
            int currentPtVersion = new Version(HostUtil.Instance.ParatextVersion).Major;
#endif

            // Grab all the available plugin manifests.
            var pluginManifests = GetPluginFilesByExtension(MainConsts.PluginManifestExtension);

            // A temporary dictionary we use to map downloaded filename (the key) to the original filenames (the value).
            // This allows us to recall what the filename pattern is for the JSON file so that we can download the plugin Zip file.
            var outputToInputMap = new Dictionary<string, string>(pluginManifests.Count);

            // Download all of the plugin manifests and marshal them into PluginDescriptions.
            var jsonFilepaths = DownloadFiles(pluginManifests, outputToInputMap);
            PluginDescriptionStore = new Dictionary<PluginDescription, string>();
            jsonFilepaths.ForEach(jsonFilepath =>
            {
                PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(File.ReadAllText(jsonFilepath));
                PluginDescriptionStore.Add(pluginDescription, outputToInputMap[jsonFilepath]);
            });

            // Filter out any plugins that aren't compatible with the current Paratext version.
            Dictionary<PluginDescription, string> compatiblePlugins = compatibleOnly
                ? PluginDescriptionStore.Where(currentPluginKvp => currentPluginKvp.Key.PtVersions.Contains(currentPtVersion.ToString())).ToDictionary(i => i.Key, i => i.Value)
                : PluginDescriptionStore;

            // Filter out older versions of plugins and return the filtered list.
            if (latestOnly)
            {
                var latestPlugins = new Dictionary<string, PluginDescription>();
                foreach (KeyValuePair<PluginDescription, string> currentPluginKvp in compatiblePlugins)
                {
                    if (!latestPlugins.ContainsKey(currentPluginKvp.Key.ShortName))
                    {
                        latestPlugins[currentPluginKvp.Key.ShortName] = currentPluginKvp.Key;
                        continue;
                    }

                    var lastPlugin = latestPlugins[currentPluginKvp.Key.ShortName];

                    var previousVersion = new Version(lastPlugin.Version);
                    var currentVersion = new Version(currentPluginKvp.Key.Version);

                    if (currentVersion > previousVersion)
                    {
                        latestPlugins[currentPluginKvp.Key.ShortName] = currentPluginKvp.Key;
                    }
                }

                return latestPlugins.Select(d => d.Value).ToList();
            }

            return compatiblePlugins.Select(d => d.Key).ToList();
        }

        /// <summary>
        /// This method returns all the files in the plugin repository with the specified extension.
        /// </summary>
        /// <param name="extension">A case-insensitive extension. If a leading '.' isn't provided, it will be added. Eg: "JSON" or ".json" will work.</param>
        /// <returns>A list of files with the specified extension.</returns>
        public virtual List<String> GetPluginFilesByExtension(string extension)
        {
            // validate input
            if (String.IsNullOrEmpty(extension))
            {
                throw new ArgumentException($"'{nameof(extension)}' must be non-null and non-empty.");
            }

            // normalize extension
            var normalizedExtension = extension.Trim().ToLower();
            if (!normalizedExtension.StartsWith("."))
            {
                // add a missing leading period
                normalizedExtension = "." + normalizedExtension;
            }

            // Get files with the specified extension from the repo. 
            return GetPluginFilenames((filename) =>
                filename.Trim().ToLower().EndsWith(normalizedExtension)
            );
        }

        /// <summary>
        /// This method retrieves the list plugin files available in the plugin repo.
        /// </summary>
        /// <param name="filenameFilter">(optional) A lambda used to whitelist files by their filename.</param>
        /// <returns>A list of plugin files available on the repo.</returns>
        public virtual List<String> GetPluginFilenames(Func<string, bool> filenameFilter = null)
        {
            List<String> pluginFilenames = new List<string>();

            // Request a list of files in the repo.
            HttpResponseMessage response = HttpClient.GetAsync(GetSignedUrl()).Result;
            {
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = $"Unable to contact PPM server. Reason Phrase: '{response.ReasonPhrase}'";
                    var additionalInfo = $"\n\n" +
                        $"\tStatus Code: {(int)response.StatusCode}\n";
                    HostUtil.Instance.LogLine(errorMessage + additionalInfo, true);
                    throw new Exception(errorMessage);

                }

                // Get the filenames from the returned XML.
                var stream = response.Content.ReadAsStreamAsync().Result;
                using XmlReader reader = XmlReader.Create(stream);
                using HttpContent content = response.Content;
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            if (reader.Name.ToString() == "Key")
                            {
                                string filename = reader.ReadElementContentAsString();
                                if (filenameFilter == null || filenameFilter(filename))
                                {
                                    pluginFilenames.Add(filename);
                                }
                            }
                        }
                    }
                }
            }

            return pluginFilenames;
        }

        /// <summary>
        /// This method downloads multiple files.
        /// </summary>
        /// <param name="filesToDownload">The list of files to download. (required)</param>
        /// <param name="outputToInputMap">(optional) A map that, for each file that has been downloaded, captures the path of the downloaded file.</param>
        /// <returns>A list of paths where the files were downloaded.</returns>
        public virtual List<string> DownloadFiles(List<string> filesToDownload, Dictionary<string, string> outputToInputMap = null)
        {
            // validate input
            _ = filesToDownload ?? throw new ArgumentNullException(nameof(filesToDownload));

            // begin downloading each file
            var fileDownloadTasks = new List<Task>(filesToDownload.Count);
            var filePaths = new List<string>(filesToDownload.Count);
            filesToDownload.ForEach(filename =>
            {
                var tempFilename = GetTemporaryAbsoluteFilePath(filename);
                fileDownloadTasks.Add(DownloadFileAsync(filename, tempFilename));
                filePaths.Add(tempFilename);

                // map the input filename to the downloaded filename if the dictionary is provided
                if (outputToInputMap != null)
                {
                    outputToInputMap[tempFilename] = Path.GetFileNameWithoutExtension(filename);
                }
            });

            // wait for all of the files to stop downloading
            Task.WaitAll(fileDownloadTasks.ToArray());

            // return the list of files
            return filePaths;
        }

        /// <summary>
        /// This method generates a temporary path to which a file can be downloaded.
        /// </summary>
        /// <param name="filename">The name of the file being downoaded.</param>
        /// <returns>The fully qualified path where the file will be downloaded.</returns>
        public virtual string GetTemporaryAbsoluteFilePath(string filename)
        {
            return Path.Combine(TemporaryDownloadDirectory.FullName, filename);
        }

        /// <summary>
        /// This method will download a file from the plugin repository and return file information about the downloaded file. 
        /// Files are saved to a temporary directory unless a <c>fileDownloadDirectory</c> is specified.
        /// </summary>
        /// <param name="fileToDownload">The filename of the file to download. (required)</param>
        /// <param name="fileDownloadDirectory">(optional) The download save directory.</param>
        /// <returns>The file information about the downloaded file.</returns>
        public virtual FileInfo DownloadFile(string fileToDownload, DirectoryInfo fileDownloadDirectory = null)
        {
            // validate input
            if (String.IsNullOrEmpty(fileToDownload))
            {
                throw new ArgumentException($"'{nameof(fileToDownload)}' must be non-null and non-empty.");
            }
            var saveFilePath = GetTemporaryAbsoluteFilePath(fileToDownload);
            // if a download directory is specified, use it.
            if (fileDownloadDirectory != null)
            {
                saveFilePath = Path.Combine(fileDownloadDirectory.FullName, fileToDownload);
            }

            // download the file and return the final path
            DownloadFileAsync(fileToDownload, saveFilePath).Wait();
            return new FileInfo(saveFilePath);
        }

        /// <summary>
        /// This method will download a file from the plugin repository. 
        /// </summary>
        /// <param name="fileToDownload">The name of the file to download.</param>
        /// <param name="fileDownloadPath">The full path (including filename) where the file should be downloaded.</param>
        /// <returns>A task representing the status of the file download.</returns>
        public virtual async Task DownloadFileAsync(string fileToDownload, string fileDownloadPath)
        {
            // validate input
            if (string.IsNullOrEmpty(fileToDownload))
            {
                throw new ArgumentException($"'{nameof(fileToDownload)}' must be non-null and non-empty.");
            }
            if (string.IsNullOrEmpty(fileDownloadPath))
            {
                throw new ArgumentException($"'{nameof(fileDownloadPath)}' must be non-null and non-empty.");
            }

            // Download the file we're interested in using a signed URL.
            var downloadUri = new Uri(GetSignedUrl(fileToDownload));
            HttpResponseMessage response = await HttpClient.GetAsync(downloadUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false); //Keeping the awaiter on the main thread prevents a deadlock.
            using FileStream file = new FileStream(fileDownloadPath, FileMode.Create, FileAccess.Write);
            {
                await response.Content.CopyToAsync(file);
            }
        }

        /// <summary>
        /// This method gets a signed URL for the specified resource. If no resource is provided, it returns a signed base URL.
        /// </summary>
        /// <param name="resource">(optional) The resource to request.</param>
        /// <returns>The signed URL.</returns>
        public virtual string GetSignedUrl(string resource = "")
        {
            string baseUrl = Resources.cloudfront_base_url;
            string policy = Resources.cloudfront_policy_document_base64;
            string signature = Resources.cloudfront_policy_document_signed;
            string keyId = Resources.cloudfront_keypair_id;

            return $"{baseUrl}/{resource}?Policy={policy}&Signature={signature}&Key-Pair-Id={keyId}";
        }
    }
}
