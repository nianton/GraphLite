using System;
using System.Threading.Tasks;

namespace GraphLite
{
    internal sealed class DelegateAuthProvider : IAuthProvider
    {
        private readonly Func<string, Task<TokenWrapper>> _authenticationDelegate;

        public DelegateAuthProvider(Func<string, Task<TokenWrapper>> authenticationDelegate)
        {
            if (authenticationDelegate == null)
                throw new ArgumentNullException(nameof(authenticationDelegate));

            _authenticationDelegate = authenticationDelegate;
        }

        public Task<TokenWrapper> GetAccessTokenAsync(string resource)
        {
            return _authenticationDelegate.Invoke(resource);
        }
    }
}
