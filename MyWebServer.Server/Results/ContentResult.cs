namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Http;

    public class ContentResult : ActionResult
    {
        public ContentResult(
            HttpResponse response, 
            string content, 
            string contentType)
            : base(response)
        => SetContent(content, contentType);
        
    }
}
