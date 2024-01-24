namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Http;
    using System.Text;
    public class TextResult : ContentResult
    {
        public TextResult(HttpResponse response, string text) 
            : base(response, text, HttpContentType.PlainText)
        {
        }
    }
}
