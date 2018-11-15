using System;
using System.Net.Http;

namespace GraphLite
{
    public static class GraphLiteConfiguration
    {
        private static string _authorityUrlRoot = "https://login.windows.net/";

        /// <summary>
        /// The azure graph resource identifier.
        /// </summary>
        public const string AzureADGraphApiRoot = "https://graph.windows.net";

        public const string BaseUrlFormat = AzureADGraphApiRoot + "/{0}";

        /// <summary>
        /// The predefined B2C application responsible the tenant's management.
        /// </summary>
        public const string B2cExtensionsApplicationName = "b2c-extensions-app";

        /// <summary>
        /// Max thumbnail photo size in bytes.
        /// </summary>
        public const int MaxThumbnailPhotoSize = 100_000;

        /// <summary>
        /// The default Graph API version.
        /// </summary>
        public static string DefaultGraphApiVersion { get; set; } = "1.6";

        public static string AuthorityUrlRoot
        {
            get => _authorityUrlRoot;
            set => _authorityUrlRoot = value.EndsWith("/") ? value : $"{value}/";
        }

        /// <summary>
        /// Gets the authorization token url for the given tenant.
        /// </summary>
        /// <param name="tenant"></param>
        public static string AuthTokenEndpoint(string tenant, bool v2 = false) =>
            WellKnownEndpointsUrl(tenant, v2 ? EndpointPaths.TokenV2 : EndpointPaths.Token);

        public static string WellKnownEndpointsUrl(string tenant, EndpointPaths path) =>
            $"{AuthorityUrlRoot}{tenant}/{path.Value}";
    }
}