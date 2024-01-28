
namespace MyWebServer.Server.Http.Collections
{
    using MyWebServer.Server.Common;
    using System.Collections;

    public class HttpFormCollection : IEnumerable<string>
    {
        private readonly Dictionary<string, string> form;

        public HttpFormCollection()
            => form = new(StringComparer.InvariantCultureIgnoreCase);

        public int Count
        {
            get => form.Count;
        }

        public void Add(string key, string value)
        {
            Guard.AgainsNull(key, nameof(key));
            Guard.AgainsNull(value, nameof(value));

            form[key] = value;
        }

        public bool Contains(string name) => form.ContainsKey(name);

        public string Get(string name)
        {
            if (!Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return form[name];
        }

        public IEnumerator<string> GetEnumerator()
            => form.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public string GetValueOrDefault(string key)
            => form.GetValueOrDefault(key);

        public string this[string name] => form[name];
    }
}
