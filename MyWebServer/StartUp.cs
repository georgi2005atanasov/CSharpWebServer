namespace MyWebServer
{
    using MyWebServer.Controllers;
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    class StartUp
    {
        static async Task Main()
            => await new HttpServer(
                routes => routes
                .MapStaticFiles()
                .MapControllers()
                .MapPost<CatsController>("/Cats/Save", c => c.Save()))
            .Start();
    }
}
