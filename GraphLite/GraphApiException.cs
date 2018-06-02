using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace GraphLite
{
    public class GraphApiException : Exception
    {
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

    public class GraphAuthException : Exception
    {
        public GraphAuthException(HttpRequestMessage request, HttpResponseMessage response, ErrorInfo error)
            : base($"Error Calling the Graph API. Code: {error?.Error} \n")
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
