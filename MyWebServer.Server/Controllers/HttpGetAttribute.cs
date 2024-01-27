namespace MyWebServer.Server.Controllers
{
    using MyWebServer.Server.Http;

    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute() 
            : base(HttpMethod.Get)
        {
        }
    }
}
