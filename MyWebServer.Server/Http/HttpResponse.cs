namespace MyWebServer.Server
{
    using MyWebServer.Server.Http;
    using System.Text;

    public abstract class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpHeadersCollection Headers { get; set; } = new HttpHeadersCollection();

        public string? Content { get; set; }

        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            this.Headers.Add("Server", "My Web Server");
            this.Headers.Add("Date", $"{DateTime.UtcNow:r}");
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers)
            {
                result.AppendLine(header.ToString());
            }

            if (!string.IsNullOrEmpty(Content))
            {
                result.AppendLine();

                result.Append(Content);
            }

            return result.ToString();
        }
    }
}
