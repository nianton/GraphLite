using System;
using System.Net.Http;

namespace GraphLite
{
    public static class GraphLiteConfiguration
    {
        /// <summary>
        /// The default Azure AD authority URL.
        /// </summary>
        public const string AzureADAuthorityUrl = "https://login.windows.net";

        /// <summary>
        /// The azure graph resource identifier.
        /// </summary>
        public const string AzureADGraphApiRoot = "https://graph.windows.net";

        /// <summary>
        /// Gets or sets the authority URL root used by the internal AuthProvider, defaults to <see cref="AzureADAuthorityUrl"/>.
        /// </summary>
        public static string AuthorityUrlRoot
        {
            get => _authorityUrlRoot;
            set => _authorityUrlRoot = value?.TrimEnd('/');
        }
        private static string _authorityUrlRoot = AzureADAuthorityUrl;

        /// <summary>
        /// Get the graph API base URL for the given tenant, eg: https://graph.windows.net/mytenant.onmicrosoft.com
        /// </summary>
        /// <param name="tenant">The tenant's identifier, eg: 'mytenant.onmicrosoft.com'</param>
        public static string TenantGraphApiBaseUrl(string tenant) =>
            $"{AzureADGraphApiRoot}/{tenant}";


        /// <summary>
        /// Get the graph API base URL for the given tenant, eg: https://graph.windows.net/mytenant.onmicrosoft.com
        /// </summary>
        /// <param name="tenant">The tenant's identifier, eg: 'mytenant.onmicrosoft.com'</param>
        public static string TenantB2cAdminApiBaseUrl(string tenant) =>
            $"{AzureADGraphApiRoot}/{tenant}";


        /// <summary>
        /// Gets the authorization token url for the given tenant.
        /// </summary>
        /// <param name="tenant">The tenant's identifier, eg: 'mytenant.onmicrosoft.com'</param>
        /// <param name="v2">Whether to return the version 2.0 endpoint</param>
        public static string AuthTokenEndpoint(string tenant, bool v2 = false) =>
            WellKnownEndpointsUrl(tenant, v2 ? EndpointPath.TokenV2 : EndpointPath.Token);

        /// <summary>
        /// Wells the known endpoints URL.
        /// </summary>
        /// <param name="tenant">The tenant's identifier, eg: 'mytenant.onmicrosoft.com'</param>
        /// <param name="path">The endpoint path.</param>
        public static string WellKnownEndpointsUrl(string tenant, EndpointPath path) =>
            $"{AuthorityUrlRoot}/{tenant}/{path.Value}";
    }
}