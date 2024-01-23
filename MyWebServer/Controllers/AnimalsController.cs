﻿
namespace MyWebServer.Controllers
{
    using MyWebServer.Models.Animals;
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;

    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            const string ageKey = "Age";
            var query = this.Request.Query;

            var catName = query.Contains(nameKey.ToLower())
            ? query[nameKey.ToLower()]
            : "the cats";

            var catAge = query.Contains(ageKey.ToLower())
                ? int.Parse(query[ageKey.ToLower()])
                : 0;

            var viewModel = new CatViewModel
            {
                Name = catName,
                Age = catAge
            };

            return View(viewModel);
        }

        public HttpResponse Dogs() => View();
        public HttpResponse Bunnies() => View("Rabbits");
        public HttpResponse Turtles() => View("Animals/Wild/Turtles");
    }
}
