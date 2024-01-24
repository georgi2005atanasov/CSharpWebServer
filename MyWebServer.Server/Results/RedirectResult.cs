namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Http;
    public class RedirectResult : ActionResult
    {
        public RedirectResult(HttpResponse response, string location)
            : base(response)
        {
            this.StatusCode = HttpStatusCode.Found;
            this.Headers.Add(HttpHeader.Location, location);
        }
    }
}
