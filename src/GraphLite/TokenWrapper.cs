using System;

namespace GraphLite
{
    /// <summary>
    /// Wrapper class to include the access token along with its expiration date.
    /// </summary>
    public struct TokenWrapper
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the expiration date of the token.
        /// </summary>
        public DateTimeOffset? Expiry { get; set; }
    }
}