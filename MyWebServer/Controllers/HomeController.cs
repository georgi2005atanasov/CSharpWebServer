
namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Results;

    public class HomeController : Controller
    {
        public HomeController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Index()
        => Text("Hello from Georgi!");

        public HttpResponse ToLocalRedirect()
        => Redirect("/Cats");

        public HttpResponse ToSoftuni()
        => Redirect("https://softuni.bg");
    }
}
