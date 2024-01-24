namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Http;

    public class NotFoundResult : ActionResult
    {
        public NotFoundResult(HttpResponse response)
            : base(response) 
            => this.StatusCode = HttpStatusCode.NotFound;
    }
}
