namespace MyWebServer.Server.Controllers
{
    using MyWebServer.Server.Http;

    public class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute()
            : base(HttpMethod.Post)
        {
        }
    }
}
