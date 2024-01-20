using MyWebServer.Server.Http;
using MyWebServer.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Controllers
{
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
        => Request = request;
        protected HttpRequest Request { get; private init; }

        protected HttpResponse Text(string text)
        => new TextResponse(text);

        protected HttpResponse Html(string text)
        => new HtmlResponse(text);

        protected HttpResponse Redirect(string location)
        => new RedirectResponse(location);
    }
}
