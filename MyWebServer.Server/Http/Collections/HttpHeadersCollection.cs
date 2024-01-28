namespace MyWebServer.Server.Http.Collections
{
    using MyWebServer.Server.Common;
    using System.Collections;

    public class HttpHeadersCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeadersCollection()
        => headers = new(StringComparer.InvariantCultureIgnoreCase);

        public int Count
        {
            get => headers.Count;
        }

        public void Add(string name, string value)
        {
            Guard.AgainsNull(name, nameof(name));
            Guard.AgainsNull(value, nameof(value));

            var header = new HttpHeader(name, value);

            headers[header.Name] = header;
        }

        public bool Contains(string name) => headers.ContainsKey(name);

        public HttpHeader Get(string name)
        {
            if (!Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return headers[name];
        }

        public HttpHeader this[string name] => headers[name];

        public IEnumerator<HttpHeader> GetEnumerator()
            => headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
