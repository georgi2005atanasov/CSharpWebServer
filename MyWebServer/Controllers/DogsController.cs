namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;
    using MyWebServer.Views.Animals;

    public class DogsController : Controller
    {
        public DogsController(HttpRequest request) 
            : base(request)
        {
        }

        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Create(DogFormModel model)
             => Text($"Dog: {model.Name} - {model.Age} - {model.Breed}");
    }
}
