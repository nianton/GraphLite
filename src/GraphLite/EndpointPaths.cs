namespace GraphLite
{
    public class EndpointPath
    {
        public override string ToString()
        {
            return Value;
        }

        private EndpointPath(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static EndpointPath OpenIdConnectWellKnown { get; } = new EndpointPath(".well-known/openid-configuration");

        public static EndpointPath Keys { get; } = new EndpointPath("discovery/keys");

        public static EndpointPath OpenIdConnectWellKnownV2 { get; } = new EndpointPath("v2.0/.well-known/openid-configuration");

        public static EndpointPath Token { get; } = new EndpointPath("oauth2/token");

        public static EndpointPath TokenV2 { get; } = new EndpointPath("v2.0/oauth2/token");
    }
}