
namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;

    public class HomeController : Controller
    {
        public HttpResponse Index()
        => Text("Hello from Georgi!");

        public HttpResponse ToLocalRedirect()
        => Redirect("/Cats");

        public HttpResponse ToSoftuni()
        => Redirect("https://softuni.bg");

        public HttpResponse Error()
        => throw new InvalidOperationException("Invalid action!");

        public HttpResponse StaticFiles() => View();
    }
}
