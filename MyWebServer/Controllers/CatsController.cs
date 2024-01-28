
namespace MyWebServer.Controllers
{
    using MyWebServer.Data;
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;

    public class CatsController : Controller
    {
        private readonly IData data;

        public CatsController(IData data) 
            => this.data = data;

        public HttpResponse All()
        {
            var cats = data
                .Cats
                .ToList();

            return View(cats);
        }

        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Save(string name, int age)
        {
            return Text($"{name} - {age}");
        }
    }
}
