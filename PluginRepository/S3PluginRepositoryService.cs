using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        TransferUtility S3TransferUtility { get; }

        public S3PluginRepositoryService()
        {
            // Set up AWS S3 Client for PPM repository requests
            SetUpAwsCredentials();

            S3TransferUtility = new TransferUtility(GetS3Client());
        }

        public FileInfo DownloadPlugin(string pluginName, string pluginVersion, DirectoryInfo downloadDirectory)
        {
            return DownloadS3File($"{pluginName}-{pluginVersion}.zip");
        }

        public List<string> GetAvailablePlugins(bool latestOnly = true)
        {
            var pluginInformationFilenames = GetRepoFilenamesByExtension(".json");

            //var pluginInfoDownloadTasks = new List<Task>(pluginInformationFilenames.Count);
            //pluginInformationFilenames.ForEach(filename =>
            //{
            //    pluginInfoDownloadTasks.Add(S3TransferUtility.DownloadAsync(GetTemporaryAbsoluteFilePath(filename), bucketName, filename));
            //});

            var s3Files = DownloadS3Files(pluginInformationFilenames);
            //Task.WaitAll(pluginInfoDownloadTasks.ToArray());

            return s3Files;
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

        private FileInfo DownloadS3File(string filename)
        {
            // validate input
            if (String.IsNullOrEmpty(filename))
            {
                throw new ArgumentException($"'{nameof(filename)}' must be non-null and non-empty.");
            }

            // download the file and return the final path
            var saveFilePath = GetTemporaryAbsoluteFilePath(filename);
            S3TransferUtility.DownloadAsync(saveFilePath, bucketName, filename);

            return new FileInfo(saveFilePath);
        }

        private List<string> DownloadS3Files(List<string> filenames)
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