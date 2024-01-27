
namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;

    public class CatsController : Controller
    {
        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Save(string name, int age)
        {
            return Text($"{name} - {age}");
        }
    }
}
