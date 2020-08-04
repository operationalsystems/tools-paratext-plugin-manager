﻿using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Newtonsoft.Json;
using PpmMain.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PpmMain.PluginRepository
{
    public class S3PluginRepositoryService : IPluginRepository
    {
        // Read-only PPM user credentials and bucket name.
        RegionEndpoint region { get; }  = RegionEndpoint.USEast1;
        const String accessKey = "AKIAQDRNRZSYWRWXIHWN";
        const String secretKey = "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";
        const String bucketName = "biblica-ppm-plugin-repo";

        // The temporary AWS credentials we use for S3 requests.
        SessionAWSCredentials AwsSessionCredentials { get; set; }

        // Temporary storage location
        public DirectoryInfo TemporaryDownloadDirectory { get; }  = new DirectoryInfo(Path.Combine(Path.GetTempPath(), "PPM"));

        // Setup needed objects
        public  TransferUtility S3TransferUtility { get; set; }

        // Plugin description dictionary with the associated filename
        public Dictionary<PluginDescription, string> PluginDescriptionStore { get; private set; }

        public S3PluginRepositoryService()
        {
            // Set up the AWS credentials the S3 client will need for PPM repository requests
            SetUpAwsCredentials();

            // The transfer utility for downloading S3 files.
            SetUpS3TransferClient();
        }

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

            var result = results.First<KeyValuePair<PluginDescription, string>>();

            return DownloadS3File($"{result.Value}.zip");
        }

        public List<PluginDescription> GetAvailablePlugins(bool latestOnly = true)
        {
            // grab all the available JSON files, as they're the plugin descriptions.
            var pluginInformationFilenames = GetRepoFilenamesByExtension(".json");

            // A temporary dictionary we use to map downloaded filename (the key) to the original filenames (the value).
            // This allows us to recall what the filename pattern is for the JSON file in S3, so that we can download the plugin Zip file.
            var outputToInputMap = new Dictionary<string, string>(pluginInformationFilenames.Count);

            /// Download all of the plugin descriptions.
            var jsonFilepaths = DownloadS3Files(pluginInformationFilenames, outputToInputMap);
            PluginDescriptionStore = new Dictionary<PluginDescription, string>();
            jsonFilepaths.ForEach(jsonFilepath =>
            {
                PluginDescription pluginDescription = JsonConvert.DeserializeObject<PluginDescription>(File.ReadAllText(jsonFilepath));
                PluginDescriptionStore.Add(pluginDescription, outputToInputMap[jsonFilepath]);
            });

            if (latestOnly)
            {
                // filter out the latest version of each unique plugin
                var latestPlugins = new Dictionary<string, PluginDescription>();
                foreach (KeyValuePair<PluginDescription, string> currentPluginKvp in PluginDescriptionStore)
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

            // return the unfiltered plugins
            return PluginDescriptionStore.Select(d => d.Key).ToList();
        }

        private void SetUpAwsCredentials()
        {

            using (var stsClient = new AmazonSecurityTokenServiceClient(accessKey, secretKey, region))
            {
                var getSessionTokenRequest = new GetSessionTokenRequest
                {
                    DurationSeconds = 900 // seconds
                };

                var sessionTokenResponse = stsClient.GetSessionTokenAsync(getSessionTokenRequest);

                Task.WaitAll(sessionTokenResponse);

                var awsCredentialss= sessionTokenResponse.Result.Credentials;

                AwsSessionCredentials = new SessionAWSCredentials(
                   awsCredentialss.AccessKeyId,
                   awsCredentialss.SecretAccessKey,
                   awsCredentialss.SessionToken);
            }
        }
        private void SetUpS3TransferClient()
        {
            S3TransferUtility = new TransferUtility(GetS3Client());
        }


        private AmazonS3Client GetS3Client()
        {
            // Use the single-user credentials to interact with the various AWS services
            return new AmazonS3Client(AwsSessionCredentials, region);
        }

        private List<String> GetRepoFilenamesByExtension(string extension)
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
            return GetS3RepoFilenames((filename) => 
                filename.Trim().ToLower().EndsWith(normalizedExtension)
            );
        }

        private List<String> GetS3RepoFilenames(Func<string, bool> filenameFilter = null)
        {
            using (var s3Client = GetS3Client())
            {
                // Request a list of the available S3 objects
                var request = s3Client.ListObjectsAsync(new ListObjectsRequest()
                {
                    BucketName = bucketName
                });
                Task.WaitAll(request);

                // filter objects on filename
                List<S3Object> jsonObjects;
                if (filenameFilter != null)
                {
                    jsonObjects = request.Result.S3Objects.FindAll(s3Obj => {
                        return filenameFilter(s3Obj.Key);
                    });
                } else
                {
                    jsonObjects = request.Result.S3Objects;
                }

                // return list of object filenames
                return jsonObjects.Select(s3obj => s3obj.Key).ToList();
            }
        }

        private FileInfo DownloadS3File(string filename, DirectoryInfo outDownloadDirectory = null)
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

            // Download the file and return the save path.
            S3TransferUtility.DownloadAsync(saveFilePath, bucketName, filename);
            return new FileInfo(saveFilePath);
        }

        private List<string> DownloadS3Files(List<string> filenames, Dictionary<string, string> outputToInputMap = null)
        {
            // validate input
            _ = filenames ?? throw new ArgumentNullException(nameof(filenames));

            // download the file and return the final path
            var pluginInfoDownloadTasks = new List<Task>(filenames.Count);
            var pluginInfoFilenames = new List<string>(filenames.Count);
            filenames.ForEach(filename =>
            {
                var tempFilename = GetTemporaryAbsoluteFilePath(filename);
                pluginInfoDownloadTasks.Add(S3TransferUtility.DownloadAsync(tempFilename, bucketName, filename));
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

        private string GetTemporaryAbsoluteFilePath(string initialFilePath)
        {
            return Path.Combine(TemporaryDownloadDirectory.FullName, $"{Path.GetRandomFileName()}_{initialFilePath}");
        }
    }
}