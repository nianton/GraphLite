using System;
using System.Collections.Generic;
using System.Text;

namespace GraphLite
{
    public class AuthResult
    {
        private AuthResult() { }

        public bool IsSuccessful => Error == null;

        public ErrorInfo Error { get; private set; }

        public AuthTokenResponse TokenResponse { get; private set; }

        public static AuthResult Success(AuthTokenResponse tokenResponse)
        {
            return new AuthResult() { TokenResponse = tokenResponse };
        }

        public static AuthResult Failed(ErrorInfo error)
        {
            return new AuthResult() { Error = error };
        }
    }
}
