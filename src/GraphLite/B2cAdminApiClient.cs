using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GraphLite
{
    public partial class B2cAdminApiClient
    {
        public string Tenant { get; private set; }

        protected override string AuthResource => "http://management.core.windows.net";

        /// <summary>
        /// Initializes an instance of the GraphApiClient with the necessary application credentials.
        /// </summary>
        /// <param name="applicationId">The application identifier.</param>
        /// <param name="applicationSecret">The application secret.</param>
        /// <param name="tenant">The B2C tenant e.g. 'mytenant.onmicrosoft.com'</param>
        public B2cAdminApiClient(string applicationId, string applicationSecret, string tenant)
            : this(tenant, new DefaultAuthProvider(HttpClient, tenant, applicationId, applicationSecret))
        { }

        /// <summary>
        ///  Initializes an instance of the GraphApiClient with authorization callback.
        /// </summary>
        /// <param name="tenant">The B2C tenant e.g. 'mytenant.onmicrosoft.com'</param>
        /// <param name="authorizationCallback">Callback to provide the content of the http Authorize header and the expiry time for the token</param>
        public B2cAdminApiClient(string tenant, Func<string, Task<TokenWrapper>> authorizationCallback)
            : this(tenant, new DelegateAuthProvider(authorizationCallback))
        { }

        private B2cAdminApiClient(string tenant, IAuthProvider authProvider)
        {
            if (string.IsNullOrWhiteSpace(tenant))
                throw new ArgumentNullException(nameof(tenant));

            AuthProvider = authProvider;
            Tenant = tenant;
            BaseUrl = GraphLiteConfiguration.TenantGraphApiBaseUrl(Tenant);
        }
    }
}
