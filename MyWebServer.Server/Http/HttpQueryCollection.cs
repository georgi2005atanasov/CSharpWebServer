using System.Collections;

namespace MyWebServer.Server.Http
{
    public class HttpQueryCollection : IEnumerable<string>
    {
        private readonly Dictionary<string, string> query;

        public HttpQueryCollection()
        {
            query = new Dictionary<string, string>();
        }

        public int Count
        {
            get
            {
                return query.Count;
            }
        }

        public void Add(string key, string value)
        {
            query.Add(key, value);
        }

        public bool Contains(string name) => query.ContainsKey(name);

        public string Get(string name)
        {
            if (!this.Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return query[name];
        }

        public IEnumerator<string> GetEnumerator()
        {
            return query.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string this[string name] => query[name];
    }
}
