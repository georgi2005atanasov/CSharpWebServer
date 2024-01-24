namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Http;

    public class HtmlResult : ContentResult
    {
        public HtmlResult(HttpResponse response, string text)
            : base(response, text, HttpContentType.HtmlText)
        {
        }
    }
}
