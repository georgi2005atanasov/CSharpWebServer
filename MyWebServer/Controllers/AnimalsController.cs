
namespace MyWebServer.Controllers
{
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
            var query = this.Request.Query;

            var catName = query.ContainsKey(nameKey.ToLower())
            ? query[nameKey.ToLower()]
            : "the cats";

            return Html($"<h1>Hello from {catName}.</h1> ");
        }

        public HttpResponse Dogs()
        {
            const string nameKey = "Name";
            var query = Request.Query;

            var dogName = query.ContainsKey(nameKey.ToLower())
            ? query[nameKey.ToLower()]
            : "the dogs";

            return Html($"<h1>Hello from {dogName}.</h1> ");
        }
    }
}
