﻿namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;

    public class AccountController : Controller
    {
        public AccountController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Login()
        {
            //var user = this.db.Users.Find(username, password);

            //if (user != null)
            //{
            //    this.SignIn(user.Id);
            //    return Text("User is logged in!");
            //}

            //return Text("Invalid credentials!");

            var someUserId = "MyUserId"; // should come from the database
            this.SignIn(someUserId);

            return Text("User authenticated");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Text("User signed out!");
        }

        public HttpResponse AuthenticatedCheck()
        {
            if (this.User.IsAuthenticated)
            {
                return Text($"Authenticated user: {this.User.Id}");
            }

            return Text($"User is not authenticated!");
        }

        [Authorize]
        public HttpResponse AuthorizationCheck()
        => Text($"Current user {this.User.Id}");


        public HttpResponse CookiesCheck()
        {
            const string cookieName = "My cookie";
            if (Request.Cookies.Contains(cookieName))
            {
                var cookie = this.Request.Cookies[cookieName];

                return Text($"Cookies already exist - {cookie}");
            }

            this.Response.AddCookie("My cookie", "My value");
            this.Response.AddCookie("My second cookie", "My second value");

            return Text("Cookies set!");
        }

        public HttpResponse SessionCheck()
        {
            const string currentDateKey = "CurrentDate";

            if (this.Request.Session.ContainsKey(currentDateKey))
            {
                var currentDate = this.Request.Session[currentDateKey];

                return Text($"Stored date: {currentDate}!");
            }

            this.Request.Session[currentDateKey] = DateTime.UtcNow.ToString();

            return Text("Current date stored!");
        }
    }
}
