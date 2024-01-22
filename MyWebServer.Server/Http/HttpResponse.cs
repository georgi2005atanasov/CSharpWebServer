namespace MyWebServer.Server
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Http;
    using System.Text;

    public abstract class HttpResponse
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public HttpHeadersCollection Headers { get; protected set; } = new HttpHeadersCollection();

        public string? Content { get; protected set; }

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

        protected void PrepareContent(string content, string contentType)
        {
            Guard.AgainsNull(content, nameof(content));
            Guard.AgainsNull(contentType, nameof(contentType));

            var contentLength = Encoding.UTF8.GetByteCount(content).ToString();

            Headers.Add("Content-Type", contentType);
            Headers.Add("Content-Length", contentLength);

            this.Content = content;
        }
    }
}
