namespace MyWebServer.Server.Controllers
{
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Identity;
    using MyWebServer.Server.Results;
    using System.Runtime.CompilerServices;
    public abstract class Controller
    {
        public const string UserSessionkey = "AuthenticatedUserId";

        private UserIdentity userIdentity;

        protected HttpRequest Request { get; init; }

        protected HttpResponse Response { get; private init; } = new HttpResponse(HttpStatusCode.OK);

        protected UserIdentity User
        {
            get
            {
                if (this.userIdentity == null)
                {
                    this.userIdentity = this.Request.Session.ContainsKey(UserSessionkey)
                        ? new UserIdentity { Id = this.Request.Session[UserSessionkey] }
                        : new();
                }

                return this.userIdentity;
            }
        }

        protected void SignIn(string userId)
        {
            this.Request.Session[UserSessionkey] = userId;
            this.userIdentity = new UserIdentity { Id = userId };
        }

        protected void SignOut()
        {
            this.Request.Session.Remove(UserSessionkey);
            this.userIdentity = new();
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
