using System;
using System.Net;
using System.Net.Http;

namespace GraphLite
{
    /// <summary>
    /// The exception that is thrown when a Graph API Client response does not indicate success.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class GraphApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphApiException"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="response">The response.</param>
        /// <param name="errorResponse">The error response.</param>
        public GraphApiException(HttpRequestMessage request, HttpResponseMessage response, ErrorResponse errorResponse)
            : base($"Error Calling the Graph API. Code: {errorResponse?.Error.Code} \n")
        {
            RequestMessage = request;
            ResponseMessage = response;
            ErrorResponse = errorResponse;
        }

        /// <summary>
        /// Gets the status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode => ResponseMessage.StatusCode;

        /// <summary>
        /// Gets or sets the request message.
        /// </summary>
        public HttpRequestMessage RequestMessage { get; set; }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        public HttpResponseMessage ResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets the error response.
        /// </summary>
        public ErrorResponse ErrorResponse { get; set; }
    }
}
