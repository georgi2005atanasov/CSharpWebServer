namespace MyWebServer
{
    using MyWebServer.Controllers;
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    using MyWebServer.Server.Responses;
    class StartUp
    {
        static async Task Main()
            => await new HttpServer(
                routes => routes
                .MapGet<AnimalsController>("/", c => c.Cats())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<HomeController>("/softuni", c => c.ToSoftuni())
                .MapGet<HomeController>("/ToCats", c => c.ToLocalRedirect()))
            .Start();
    }
}
