using System;
using System.Net;

namespace Halbot.BusinessLayer.GarminConnect
{
    public class GarminConnectTooManyRequestsException : Exception
    {
        public GarminConnectTooManyRequestsException() : base("Too many requests. Try again later.")
        {
        }
    }

    public class GarminConnectRequestException : Exception
    {
        public string Url { get; }

        public HttpStatusCode Status { get; }

        public GarminConnectRequestException(string url, HttpStatusCode status) : base(
            $"Request [{url}] return code {(int)status} ({status.ToString()}).")
        {
            Url = url;
            Status = status;
        }
    }


    public class GarminConnectAuthenticationException : Exception
    {
        public GarminConnectAuthenticationException() : base("Authentication error")
        {
        }

        public GarminConnectAuthenticationException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}
