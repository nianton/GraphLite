using System;

namespace GraphLite
{
    public struct TokenWrapper
    {
        public string Token { get; set; }

        public DateTimeOffset? Expiry { get; set; }
    }
}