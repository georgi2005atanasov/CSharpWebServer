
namespace MyWebServer.Server.Http.Collections
{
    using MyWebServer.Server.Common;
    using System.Collections;

    public class HttpQueryCollection : IEnumerable<string>
    {
        private readonly Dictionary<string, string> query;

        public HttpQueryCollection()
        => query = new(StringComparer.InvariantCultureIgnoreCase);

        public int Count
        {
            get => query.Count;
        }

        public void Add(string key, string value)
        {
            Guard.AgainsNull(key, nameof(key));
            Guard.AgainsNull(value, nameof(value));

            query[key] = value;
        }

        public bool Contains(string name) => query.ContainsKey(name);

        public string Get(string name)
        {
            if (!Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return query[name];
        }

        public IEnumerator<string> GetEnumerator()
            => query.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public string GetValueOrDefault(string key)
            => query.GetValueOrDefault(key);

        public string this[string name] => query[name];
    }
}
