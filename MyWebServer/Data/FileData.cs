using MyWebServer.Data.Models;

namespace MyWebServer.Data
{
    public class FileData : IData
    {
        public IEnumerable<Cat> Cats => throw new NotImplementedException();
    }
}
