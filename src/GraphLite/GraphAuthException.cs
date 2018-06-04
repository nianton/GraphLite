using System;
using System.Net;
using System.Net.Http;

namespace GraphLite
{
    /// <summary>
    /// The expeption that is thrown when authorization fails for the GraphApiClient.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class GraphAuthException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphAuthException"/> class.
        /// </summary>
        /// <param name="request">The request message.</param>
        /// <param name="response">The response message.</param>
        /// <param name="error">The error.</param>
        public GraphAuthException(HttpRequestMessage request, HttpResponseMessage response, ErrorInfo error)
            : base($"Error Authenticating the application's credentials. Code: {error?.Error} \n")
        {
            
            RequestMessage = request;
            ResponseMessage = response;
            Error = error;
        }

        public HttpStatusCode StatusCode => ResponseMessage.StatusCode;
        public HttpRequestMessage RequestMessage { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }
        public ErrorInfo Error { get; set; }
    }
}
