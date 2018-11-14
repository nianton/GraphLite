using System;
using System.Threading.Tasks;

namespace GraphLite
{
    internal static class InputValidator
    {
        internal static void ValidateInput(string applicationId, string applicationSecret, string tenant)
        {
            if (string.IsNullOrWhiteSpace(applicationId)) throw new ArgumentNullException("applicationId");
            if (string.IsNullOrWhiteSpace(applicationSecret)) throw new ArgumentNullException("applicationSecret");
            if (!Guid.TryParse(applicationId, out _)) throw new ArgumentException("invalid format", "applicationId");
            if (string.IsNullOrWhiteSpace(tenant)) throw new ArgumentNullException("tenant");
        }

        internal static void ValidateInput(string tenant, Func<string, Task<TokenWrapper>> authorizationCallback)
        {
            if (authorizationCallback == null) throw new ArgumentNullException("authorizationCallback");
            if (string.IsNullOrWhiteSpace(tenant)) throw new ArgumentNullException("tenant");
        }
    }
}