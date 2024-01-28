using MyWebServer.Data.Models;

namespace MyWebServer.Data
{
    public interface IData
    {
        IEnumerable<Cat> Cats { get; }
    }
}
