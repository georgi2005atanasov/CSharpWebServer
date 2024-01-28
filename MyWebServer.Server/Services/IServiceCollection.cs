namespace MyWebServer.Server.Services
{
    public interface IServiceCollection
    {
        IServiceCollection Add<TService, TImplementation>()
            where TService : class
            where TImplementation : TService;
    }
}
