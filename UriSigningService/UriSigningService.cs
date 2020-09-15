using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using static PpmMain.UriSigning.Utils;

namespace PpmMain.UriSigning
{
    public class UriSigningService : IUriSigningService
    {
        public Uri CreateSignedUri(string resource)
        {
            return new Uri(SignURI(resource, "seconds", "30", @"policy.txt", @"key.pem", "key-id"));
        }

        /// <summary>
        /// This method signs a URI.
        /// </summary>
        /// <param name="resource">The resource (URI) to be converted into a signed URI</param>
        /// <param name="durationUnits">The unit type for the duration (eg "minutes", "seconds", etc)</param>
        /// <param name="durationNumber">The number of units</param>
        /// <param name="pathToPolicyStmnt">The path to the policy statement template</param>
        /// <param name="pathToPrivateKey">The path to the private key used for signing the URI</param>
        /// <param name="privateKeyId">The ID of the private key (eg CloudFront key pair ID)</param>
        /// <returns>The signed URI</returns>
        public static string SignURI(string resource,
            string durationUnits, string durationNumber, string pathToPolicyStmnt,
            string pathToPrivateKey, string privateKeyId)
        {
            TimeSpan timeSpanInterval = GetDuration(durationUnits, durationNumber);

            // Create the policy statement using an existing policy statement template.
            string strPolicy = CreatePolicyStatement(pathToPolicyStmnt,
                resource,
                DateTime.Now,
                DateTime.Now.Add(timeSpanInterval),
                "0.0.0.0/0");
            if ("Error!" == strPolicy) return "Invalid time frame." +
                "Start time cannot be greater than end time.";

            // Copy the expiration time defined by policy statement.
            string strExpiration = CopyExpirationTimeFromPolicy(strPolicy);

            // Read the policy into a byte buffer.
            byte[] bufferPolicy = Encoding.ASCII.GetBytes(strPolicy);

            // Initialize the SHA1CryptoServiceProvider object and hash the policy data.
            using (SHA1CryptoServiceProvider
                cryptoSHA1 = new SHA1CryptoServiceProvider())
            {
                bufferPolicy = cryptoSHA1.ComputeHash(bufferPolicy);

                // Initialize the RSACryptoServiceProvider object.
                RSACryptoServiceProvider providerRSA = new RSACryptoServiceProvider();
                XmlDocument xmlPrivateKey = new XmlDocument();

                // Load the private key
                StreamReader reader = new StreamReader(pathToPrivateKey);
                PemReader pemReader = new PemReader(reader);
                AsymmetricCipherKeyPair keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();
                AsymmetricKeyParameter privateKey = keyPair.Private;
                RSA rsa = DotNetUtilities.ToRSA((RsaPrivateCrtKeyParameters)privateKey);

                // Convert the private key to XML for use by .Net's RSACryptoServiceProvider.
                string xmlRsa = rsa.ToXmlString(true);
                xmlPrivateKey.LoadXml(xmlRsa);

                // Format the RSACryptoServiceProvider providerRSA and create the signature.
                providerRSA.FromXmlString(xmlPrivateKey.InnerXml);
                RSAPKCS1SignatureFormatter rsaFormatter =
                    new RSAPKCS1SignatureFormatter(providerRSA);
                rsaFormatter.SetHashAlgorithm("SHA1");
                byte[] signedPolicyHash = rsaFormatter.CreateSignature(bufferPolicy);

                // Convert the signed policy to URL-safe base64 encoding.
                string strSignedPolicy = ToUrlSafeBase64String(signedPolicyHash);

                // Concatenate the URL, the timestamp, the signature, and the key pair ID to form the signed URI.
                return resource +
                    "?Expires=" +
                    strExpiration +
                    "&Signature=" +
                    strSignedPolicy +
                    "&Key-Pair-Id=" +
                    privateKeyId;
            }
        }

        /// <summary>
        /// This method creates a policy statement using a policy statement template.
        /// </summary>
        /// <param name="policyStmnt">The policy statement template.</param>
        /// <param name="resource"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static string CreatePolicyStatement(string policyStmnt, string resource, DateTime startTime, DateTime endTime, string ipAddress)

        {
            // Create the policy statement.
            FileStream streamPolicy = new FileStream(policyStmnt, FileMode.Open, FileAccess.Read);
            using (StreamReader reader = new StreamReader(streamPolicy))
            {
                string strPolicy = reader.ReadToEnd();

                TimeSpan startTimeSpanFromNow = startTime - DateTime.Now;
                TimeSpan endTimeSpanFromNow = endTime - DateTime.Now;
                TimeSpan intervalStart =
                   DateTime.UtcNow.Add(startTimeSpanFromNow) -
                   new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan intervalEnd =
                   DateTime.UtcNow.Add(endTimeSpanFromNow) -
                   new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                int startTimestamp = (int)intervalStart.TotalSeconds; // START_TIME
                int endTimestamp = (int)intervalEnd.TotalSeconds;  // END_TIME

                if (startTimestamp > endTimestamp)
                    return "Error!";

                // Replace variables in the policy statement.
                strPolicy = strPolicy.Replace("RESOURCE", resource);
                strPolicy = strPolicy.Replace("START_TIME", startTimestamp.ToString());
                strPolicy = strPolicy.Replace("END_TIME", endTimestamp.ToString());
                strPolicy = strPolicy.Replace("IP_ADDRESS", ipAddress);
                strPolicy = strPolicy.Replace("EXPIRES", endTimestamp.ToString());
                return strPolicy;
            }
        }

        public static string CopyExpirationTimeFromPolicy(string policyStatement)
        {
            int startExpiration = policyStatement.IndexOf("EpochTime");
            string strExpirationRough = policyStatement.Substring(startExpiration +
               "EpochTime".Length);
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            List<char> listDigits = new List<char>(digits);
            StringBuilder buildExpiration = new StringBuilder(20);

            foreach (char c in strExpirationRough)
            {
                if (listDigits.Contains(c))
                    buildExpiration.Append(c);
            }
            return buildExpiration.ToString();
        }
    }
}
