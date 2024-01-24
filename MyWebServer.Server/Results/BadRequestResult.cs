namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Http;

    public class BadRequestResult : HttpResponse
    {
        public BadRequestResult() 
            : base(HttpStatusCode.BadRequest)
        {
        }
    }
}
