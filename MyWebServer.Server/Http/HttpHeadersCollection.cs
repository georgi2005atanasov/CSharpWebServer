namespace MyWebServer.Server.Http
{
    using System.Collections;
    public class HttpHeadersCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeadersCollection()
        {
            headers = new Dictionary<string, HttpHeader>(StringComparer.InvariantCultureIgnoreCase);
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

            headers[header.Name] = header;
        }

        public bool Contains(string name) => headers.ContainsKey(name);

        public HttpHeader Get(string name)
        {
            if (!this.Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return headers[name];
        }

        public HttpHeader this[string name] => headers[name];

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
