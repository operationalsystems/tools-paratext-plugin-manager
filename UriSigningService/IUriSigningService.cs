using System;

namespace PpmMain.UriSigning
{
    /// <summary>
    /// A service that handles creating signed URIs.
    /// </summary>
    interface IUriSigningService
    {
        /// <summary>
        /// This method returns a signed URI for a given resource.
        /// </summary>
        /// <param name="resource">A fully-qualified resource (eg "https://somedomain.com/someresource")</param>
        /// <returns>A signed URI</returns>
        public Uri CreateSignedUri(string resource);
    }
}
