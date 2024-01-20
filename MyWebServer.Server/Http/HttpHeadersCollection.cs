namespace MyWebServer.Server.Http
{
    using System.Collections;
    public class HttpHeadersCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeadersCollection()
        {
            headers = new Dictionary<string, HttpHeader>();
        }

        public int Count
        {
            get
            {
                return headers.Count;
            }
        }

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);

            headers.Add(header.Name, header);
        }

        public IEnumerator<HttpHeader> GetEnumerator()
        {
            return headers.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
