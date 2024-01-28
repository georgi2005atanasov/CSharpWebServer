namespace MyWebServer
{
    using MyWebServer.Controllers;
    using MyWebServer.Data;
    using MyWebServer.Server;
    using MyWebServer.Server.Controllers;
    class StartUp
    {
        static async Task Main()
            => await HttpServer
            .WithRoutes(routes =>
                 routes
                .MapStaticFiles()
                .MapControllers())
            .WithServices(services => services.Add<IData, MyDbContext>())
            .Start();
    }
}
