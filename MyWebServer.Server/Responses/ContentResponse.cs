using MyWebServer.Server.Common;
using MyWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Responses
{
    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainsNull(text);

            var contentLength = Encoding.UTF8.GetByteCount(text).ToString();

            Headers.Add("Content-Type", contentType);
            Headers.Add("Content-Length", contentLength);

            this.Content = text;
        }
    }
}
