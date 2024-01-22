namespace MyWebServer.Server.Responses
{
    using MyWebServer.Server.Http;

    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse()
            : base(HttpStatusCode.NotFound)
        {
            this.Content = $"View was not found.";
        }
    }
}
