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
    public class PluginRepositoryService : IPluginRepository
    {
        // Read-only PPM user credentials and bucket name.
        RegionEndpoint region = RegionEndpoint.USEast1;
        const String accessKey = "AKIAQDRNRZSYWRWXIHWN";
        const String secretKey = "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";
        const String bucketName = "biblica-ppm-plugin-repo";

        // The AWS credentials we se to make temporary session credentials
        Credentials awsCredentials;



        public PluginRepositoryService()
        {
            // Set up AWS S3 Client for PPM repository requests
            SetUpAwsCredentials();

        }

        public FileInfo DownloadPlugin(string pluginName, string pluginVersion, DirectoryInfo downloadDirectory)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAvailablePlugins(bool latestOnly = true)
        {
            return GetRepoFilenamesByExtension(".json");
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

                awsCredentials = sessionTokenResponse.Result.Credentials;
            }
        }

        private SessionAWSCredentials GetAwsSessionCredentials()
        {
             return new SessionAWSCredentials(
                awsCredentials.AccessKeyId,
                awsCredentials.SecretAccessKey,
                awsCredentials.SessionToken);
        }

        private AmazonS3Client GetS3Client()
        {
            // Use the single-user credentials to interact with the various AWS services
            return new AmazonS3Client(GetAwsSessionCredentials(), region);
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

            // Get 
            return GetRepoFilenames((filename) => 
                filename.Trim().ToLower().EndsWith(normalizedExtension)
            );
        }

        private List<String> GetRepoFilenames(Func<string, bool> filenameFilter = null)
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

        private async void DownloadS3File(string filename)
        {
            // validate input
            _ = filename ?? throw new ArgumentNullException(nameof(filename));

            using (var s3Client = GetS3Client())
            {
                // Request an object download
                var request = s3Client.GetObjectAsync(new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = filename
                });
                Task.WaitAll(request);


                // return list of object filenames
                //return jsonObjects.Select(s3obj => s3obj.Key).ToList();
            }

            //var responseBody = "";
            //    GetObjectRequest request = new GetObjectRequest
            //    {
            //        BucketName = bucketName,
            //        Key = filename
            //    };
            //    using (GetObjectResponse response = await GetS3Client().GetObjectAsync(request))
            //    using (Stream responseStream = response.ResponseStream)
            //    using (StreamReader reader = new StreamReader(responseStream))
            //    {
            //        string title = response.Metadata["x-amz-meta-title"]; // Assume you have "title" as medata added to the object.
            //        string contentType = response.Headers["Content-Type"];
            //        Console.WriteLine("Object metadata, Title: {0}", title);
            //        Console.WriteLine("Content type: {0}", contentType);

            //        responseBody = reader.ReadToEnd(); // Now you process the response body.
            //    }
        }
    }
}