using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

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

        public HttpStatusCode StatusCode => ResponseMessage.StatusCode;
        public HttpRequestMessage RequestMessage { get; set; }
        public HttpResponseMessage ResponseMessage { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}
