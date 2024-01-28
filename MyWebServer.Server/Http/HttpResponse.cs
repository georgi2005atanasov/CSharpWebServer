namespace MyWebServer.Server
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Http.Collections;
    using System.Text;

    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; protected set; }

        public HttpHeadersCollection Headers { get; set; } = new HttpHeadersCollection();

        public HttpCookieCollection Cookies { get; private set; } = new HttpCookieCollection();

        public byte[] Content { get; protected set; }

        public bool HasContent => this.Content != null && this.Content.Any();

        public HttpResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;

            this.Headers.Add(HttpHeader.Server, "My Web Server");
            this.Headers.Add(HttpHeader.Date, $"{DateTime.UtcNow:r}");
        }

        public HttpResponse SetContent(string content, string contentType)
        {
            Guard.AgainsNull(content, nameof(content));
            Guard.AgainsNull(contentType, nameof(contentType));

            var contentLength = Encoding.UTF8.GetByteCount(content).ToString();

            this.Headers.Add(HttpHeader.ContentType, contentType);
            this.Headers.Add(HttpHeader.ContentLength, contentLength);

            this.Content = Encoding.UTF8.GetBytes(content);

            return this;
        }

        public HttpResponse SetContent(byte[] content, string contentType)
        {
            Guard.AgainsNull(content, nameof(content));
            Guard.AgainsNull(contentType, nameof(contentType));

            this.Headers.Add(HttpHeader.ContentType, contentType);
            this.Headers.Add(HttpHeader.ContentLength, content.Length.ToString());

            this.Content = content;

            return this;
        }

        public static HttpResponse ForError(string message)
            => new HttpResponse(HttpStatusCode.InternalServerError)
            {
                Content = Encoding.UTF8.GetBytes(message),
            };

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

            if (this.HasContent)
            {
                result.AppendLine();
            }
            return result.ToString();
        }
    }
}
