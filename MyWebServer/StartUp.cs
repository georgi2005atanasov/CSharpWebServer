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
                .MapGet<AnimalsController>("/", c => c.Cats())
                .MapGet<AnimalsController>("/Cats", c => c.Cats())
                .MapGet<AnimalsController>("/Dogs", c => c.Dogs())
                .MapGet<HomeController>("/softuni", c => c.ToSoftuni())
                .MapGet<HomeController>("/ToCats", c => c.ToLocalRedirect())
                .MapGet<AnimalsController>("/Turtles", c => c.Turtles())
                .MapGet<AnimalsController>("/Bunnies", c => c.Bunnies())
                .MapGet<CatsController>("/Cats/Create", c => c.Create())
                .MapGet<AccountController>("/Cookies", c => c.ActionWithCookies())
                .MapPost<CatsController>("/Cats/Save", c => c.Save()))
            .Start();
    }
}
