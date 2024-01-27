namespace MyWebServer.Server.Controllers
{
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Identity;
    using MyWebServer.Server.Results;
    using System.Runtime.CompilerServices;
    public abstract class Controller
    {
        public const string UserSessionkey = "AuthenticatedUserId";

        protected Controller(HttpRequest request)
        {
            Request = request;

            this.User = this.Request.Session.ContainsKey(UserSessionkey)
                ? new UserIdentity { Id = this.Request.Session[UserSessionkey] }
                : new();
        }

        protected HttpRequest Request { get; private init; }

        protected HttpResponse Response { get; private init; } = new HttpResponse(HttpStatusCode.OK);

        protected UserIdentity User { get; private set; } = new();

        protected void SignIn(string userId)
        {
            this.Request.Session[UserSessionkey] = userId;
            this.User = new UserIdentity { Id = userId };
        }

        protected void SignOut()
        {
            this.Request.Session.Remove(UserSessionkey);
            this.User = new();
        }

        protected ActionResult Text(string text)
        => new TextResult(this.Response, text);

        protected ActionResult Html(string text)
        => new HtmlResult(this.Response, text);

        protected ActionResult Redirect(string location)
        => new RedirectResult(this.Response, location);

        protected ActionResult View([CallerMemberName] string viewName = "")
        => new ViewResult(this.Response, viewName, this.GetType().GetControllerName(), null);

        protected ActionResult View(string viewName, object model)
        => new ViewResult(this.Response, viewName, this.GetType().GetControllerName(), model);

        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
        => new ViewResult(this.Response, viewName, this.GetType().GetControllerName(), model);

    }
}
