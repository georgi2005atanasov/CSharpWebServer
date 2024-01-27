namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Views.Animals;

    public class DogsController : Controller
    {
        [HttpGet]
        public HttpResponse Create() => View();

        [HttpPost]
        public HttpResponse Create(DogFormModel model)
             => Text($"Dog: {model.Name} - {model.Age} - {model.Breed}");
    }
}
