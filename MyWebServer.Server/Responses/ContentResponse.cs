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
        public ContentResponse(string content, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainsNull(content, nameof(content));
            Guard.AgainsNull(contentType, nameof(contentType));

            PrepareContent(content, contentType);
        }
    }
}
