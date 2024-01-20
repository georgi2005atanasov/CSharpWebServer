
namespace MyWebServer.Controllers
{
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Responses;

    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request) 
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "name";
            var query = this.Request.Query;

            var catName = query.ContainsKey(nameKey)
            ? query[nameKey]
            : "the cats";

            return Html($"<h1>Hello from angry {catName}.</h1> ");
        }

        public HttpResponse Dogs()
        {
            const string nameKey = "name";
            var query = Request.Query;

            var dogName = query.ContainsKey(nameKey)
            ? query[nameKey]
            : "the dogs";

            return Html($"<h1>Hello from {dogName}.</h1> ");
        }
    }
}
