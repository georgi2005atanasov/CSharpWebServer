namespace MyWebServer.Server.Responses
{
    using MyWebServer.Server.Http;

    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text)
            : base(text, HttpContentType.HtmlText)
        {
        }
    }
}
