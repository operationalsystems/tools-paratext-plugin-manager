using Newtonsoft.Json;
using PpmMain.Models;
using PpmMain.UriSigning;
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
    class HTTPPluginRepositoryService : IPluginRepository
    {
        static readonly HttpClient httpClient = new HttpClient();

        /// <summary>
        /// The directory used by default to download files from the plugin repository.
        /// </summary>
        public virtual DirectoryInfo TemporaryDownloadDirectory { get; private set; } = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "PPM"));

        /// <summary>
        /// The service that will sign URLs.
        /// </summary>
        public UriSigningService signingService = new UriSigningService();

        /// <summary>
        /// The plugin description dictionary used to map the associated filenames on the repository.
        /// </summary>
        public Dictionary<PluginDescription, string> PluginDescriptionStore { get; private set; }

        public FileInfo DownloadPlugin(string pluginShortname, string pluginVersion, DirectoryInfo downloadDirectory = null)
        {
            // validate input
            _ = pluginShortname ?? throw new ArgumentNullException(nameof(pluginShortname));
            _ = pluginVersion ?? throw new ArgumentNullException(nameof(pluginVersion));

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

            // grab all the available JSON files, as they're the plugin descriptions.
            var pluginInformationFilenames = GetPluginFilenamesByExtension(MainConsts.PluginManifestExtension);

            // A temporary dictionary we use to map downloaded filename (the key) to the original filenames (the value).
            // This allows us to recall what the filename pattern is for the JSON file so that we can download the plugin Zip file.
            var outputToInputMap = new Dictionary<string, string>(pluginInformationFilenames.Count);

            /// Download all of the plugin descriptions.
            var jsonFilepaths = DownloadDescriptionFiles(pluginInformationFilenames, outputToInputMap);
            PluginDescriptionStore = new Dictionary<PluginDescription, string>();
            jsonFilepaths.ForEach(jsonFilepath =>
            {
                PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(File.ReadAllText(jsonFilepath));
                PluginDescriptionStore.Add(pluginDescription, outputToInputMap[jsonFilepath]);
            });

            Dictionary<PluginDescription, string> compatiblePlugins = compatibleOnly
                ? PluginDescriptionStore.Where(currentPluginKvp => currentPluginKvp.Key.PtVersions.Contains(currentPtVersion.ToString())).ToDictionary(i => i.Key, i => i.Value)
                : PluginDescriptionStore;

            if (latestOnly)
            {
                // filter out the latest version of each unique plugin
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

                // return the latest only plugins
                return latestPlugins.Select(d => d.Value).ToList();
            }

            // return the compatible plugins
            return compatiblePlugins.Select(d => d.Key).ToList();
        }

        /// <summary>
        /// This function will return the filenames of all files that have a specified extension from the plugin repository.
        /// </summary>
        /// <param name="extension">A case-insensitive extension. If a leading '.' isn't provided, it will be added. Eg: "JSON" or ".json" will work. (required)</param>
        /// <returns>A list of filenames with the specified extension.</returns>
        public virtual List<String> GetPluginFilenamesByExtension(string extension)
        {
            // validate input
            _ = extension ?? throw new ArgumentNullException(nameof(extension));

            // normalize extension
            var normalizedExtension = extension.Trim().ToLower();
            if (!normalizedExtension.StartsWith("."))
            {
                // add a missing leading period
                normalizedExtension = "." + normalizedExtension;
            }

            // Get files from the repo by the extension filter. 
            return GetPluginFilenames((filename) =>
                filename.Trim().ToLower().EndsWith(normalizedExtension)
            );
        }

        /// <summary>
        /// This function will retrieve a list of filenames, and whitelist them by a provided filter. If no filter is provided, all filenames are returned.
        /// </summary>
        /// <param name="filenameFilter">A lambda used to whitelist files by their filename. (optional)</param>
        /// <returns>A list of filenames available on the repo.</returns>
        private List<String> GetPluginFilenames(Func<string, bool> filenameFilter = null)
        {
            List<String> filenames = new List<string>();

            // Request a list of the available plugins
            HttpResponseMessage response = httpClient.GetAsync(MainConsts.PluginRepoUrl).Result;
            {
                // Get the filenames from the returned XML
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
                                    filenames.Add(filename);
                                }
                            }
                        }
                    }
                }
            }

            // return list of object filenames
            return filenames;
        }

        /// <summary>
        /// This is a utility function that will download multiple files based on a list of filenames. The function will also map all of the filenames on the 
        /// server to the downloaded filenames.
        /// </summary>
        /// <param name="filenames">The list of filenames for files to download. (required)</param>
        /// <param name="outputToInputMap">The dictionary used to map filenames on the repository to the filenames of the files downloaded locally. (optional)</param>
        /// <returns></returns>
        public virtual List<string> DownloadDescriptionFiles(List<string> filenames, Dictionary<string, string> outputToInputMap = null)
        {
            // validate input
            _ = filenames ?? throw new ArgumentNullException(nameof(filenames));

            // download the file and return the final path
            var pluginInfoDownloadTasks = new List<Task>(filenames.Count);
            var pluginInfoFilenames = new List<string>(filenames.Count);
            filenames.ForEach(filename =>
            {
                var tempFilename = GetTemporaryAbsoluteFilePath(filename);
                pluginInfoDownloadTasks.Add(DownloadFileAsync(tempFilename, filename));
                pluginInfoFilenames.Add(tempFilename);

                // map the input filename to the downloaded filename if the dictionary is provided
                if (outputToInputMap != null)
                {
                    outputToInputMap[tempFilename] = Path.GetFileNameWithoutExtension(filename);
                }
            });

            // wait for all of the files to stop downloading
            Task.WaitAll(pluginInfoDownloadTasks.ToArray());

            // return the list of plugin file info
            return pluginInfoFilenames;
        }

        /// <summary>
        /// This function will generate a tempoary path to which a file can be downloaded.
        /// </summary>
        /// <param name="initialFilePath">The initial filename to base the temporary fully qualified filename off of. (required)</param>
        /// <returns>The fully qualified temporary file path.</returns>
        private string GetTemporaryAbsoluteFilePath(string initialFilePath)
        {
            return Path.Combine(TemporaryDownloadDirectory.FullName, initialFilePath);
        }

        /// <summary>
        /// This function will download a file from the plugin repository and return file information about the downloaded file. 
        /// An optional output directory can be specified; Otherwise the downloaded files will be placed in a temporary directory.
        /// </summary>
        /// <param name="filename">The filename of the files to download. (required)</param>
        /// <param name="outDownloadDirectory">The download save directory. (optional)</param>
        /// <returns>The file information about the downloaded file.</returns>
        public virtual FileInfo DownloadFile(string filename, DirectoryInfo outDownloadDirectory = null)
        {
            // validate input
            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException($"'{nameof(filename)}' must be non-null and non-empty.");
            }

            // download the file and return the final path
            var saveFilePath = GetTemporaryAbsoluteFilePath(filename);

            // if a download directory is specified, use it.
            if (outDownloadDirectory != null)
            {
                saveFilePath = Path.Combine(outDownloadDirectory.FullName, filename);
            }

            DownloadFileAsync(saveFilePath, filename).Wait();

            return new FileInfo(saveFilePath);
        }

        /// <summary>
        /// This function will download a file from the plugin repository. 
        /// </summary>
        /// <param name="tempfilename">The path where the file should be downloaded, including the filename.</param>
        /// <param name="filename">The name of the file to download. (required)</param>
        /// <returns>A task representing the status of the file download.</returns>
        public async Task DownloadFileAsync(string tempfilename, string filename)
        {
            // validate input
            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException($"'{nameof(filename)}' must be non-null and non-empty.");
            }

            // Download the file we're interested in using a signed URI.
            var downloadUri = signingService.CreateSignedUri($"{MainConsts.PluginRepoUrl}/{filename}");

            HttpResponseMessage response = await httpClient.GetAsync(downloadUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            using FileStream file = new FileStream(tempfilename, FileMode.Create, FileAccess.ReadWrite);
            {
                await response.Content.CopyToAsync(file);
            }
        }
    }
}
