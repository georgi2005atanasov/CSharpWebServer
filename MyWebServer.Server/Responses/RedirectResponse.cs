﻿namespace MyWebServer.Server.Responses
{
    using MyWebServer.Server.Http;
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse(string location)
            : base(HttpStatusCode.Found)
        {
            this.Headers.Add(HttpHeader.Location, location);
        }
    }
}
