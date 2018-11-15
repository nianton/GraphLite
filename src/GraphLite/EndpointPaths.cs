namespace GraphLite
{
    public class EndpointPaths
    {
        public override string ToString()
        {
            return Value;
        }

        private EndpointPaths(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static EndpointPaths OpenIdConnectWellKnown { get; } = new EndpointPaths(".well-known/openid-configuration");

        public static EndpointPaths Keys { get; } = new EndpointPaths("discovery/keys");

        public static EndpointPaths OpenIdConnectWellKnownV2 { get; } = new EndpointPaths("v2.0/.well-known/openid-configuration");

        public static EndpointPaths Token { get; } = new EndpointPaths("oauth2/token");

        public static EndpointPaths TokenV2 { get; } = new EndpointPaths("v2.0/oauth2/token");
    }
}