using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    /// <summary>
    /// The interface representing an authorization provider for Graph API.
    /// </summary>
    public interface IAuthProvider
    {
        /// <summary>
        /// Gets the access token required for the Azure AD Graph API asynchronously.
        /// </summary>
        /// <param name="resource">The resource to be granted access (<see cref="GraphLiteConfiguration.AzureADGraphApiRoot"/>).</param>
        /// <returns>A token wrapper containing the access token and its expiration.</returns>
        Task<TokenWrapper> GetAccessTokenAsync(string resource);
    }
}
