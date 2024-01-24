namespace MyWebServer.Server.Controllers
{
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Results;
    using System.Runtime.CompilerServices;
    public abstract class Controller
    {
        protected Controller(HttpRequest request)
        {
            Request = request;
            this.Response = new HttpResponse(HttpStatusCode.OK);
        }

        protected HttpRequest Request { get; private init; }

        protected HttpResponse Response { get; private init; }

        protected ActionResult Text(string text)
        => new TextResult(this.Response, text);

        protected ActionResult Html(string text)
        => new HtmlResult(this.Response, text);

        protected ActionResult Redirect(string location)
        => new RedirectResult(this.Response, location);

        protected ActionResult View([CallerMemberName] string viewName = "")
        => new ViewResult(this.Response, viewName, GetControllerName(), null);

        protected ActionResult View(string viewName, object model)
        => new ViewResult(this.Response, viewName, GetControllerName(), model);

        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
        => new ViewResult(this.Response, viewName, GetControllerName(), model);

        private string GetControllerName()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}
