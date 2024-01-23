namespace MyWebServer.Server.Controllers
{
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Responses;
    using System.Runtime.CompilerServices;
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

        protected HttpResponse View([CallerMemberName] string viewName = "")
        => new ViewResponse(viewName, GetControllerName(), null);

        protected HttpResponse View(string viewName, object model)
        => new ViewResponse(viewName, GetControllerName(), model);

        protected HttpResponse View(object model, [CallerMemberName] string viewName = "")
        => new ViewResponse(viewName, GetControllerName(), model);

        private string GetControllerName()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}
