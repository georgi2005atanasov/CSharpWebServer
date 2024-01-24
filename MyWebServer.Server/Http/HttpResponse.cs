namespace MyWebServer.Server
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Http;
    using System.Text;

    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public HttpHeadersCollection Headers { get; set; } = new HttpHeadersCollection();

        public HttpCookieCollection Cookies { get; private set; } = new HttpCookieCollection();

        public string? Content { get; protected set; }

        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            this.AddHeader(HttpHeader.Server, "My Web Server");
            this.AddHeader(HttpHeader.Date, $"{DateTime.UtcNow:r}");
        }

        public void AddHeader(string name, string value)
        {
            Guard.AgainsNull(name, nameof(name));
            Guard.AgainsNull(value, nameof(value));

            this.Headers.Add(name, value);
        }

        public void AddCookie(string name, string value)
        {
            Guard.AgainsNull(name, nameof(name));
            Guard.AgainsNull(value, nameof(value));

            this.Cookies.Add(name, value);
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            result.AppendLine($"HTTP/1.1 {(int)StatusCode} {StatusCode}");

            foreach (var header in Headers)
            {
                result.AppendLine(header.ToString());
            }

            foreach (var cookie in Cookies)
            {
                result.AppendLine($"{HttpHeader.SetCookie}: {cookie.ToString()}");
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

            AddHeader(HttpHeader.ContentType, contentType);
            AddHeader(HttpHeader.ContentLength, contentLength);

            this.Content = content;
        }
    }
}
