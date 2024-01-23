using System.Collections;

namespace MyWebServer.Server.Http
{
    public class HttpFormCollection : IEnumerable<string>
    {
        private readonly Dictionary<string, string> form;

        public HttpFormCollection()
        {
            form = new Dictionary<string, string>();
        }

        public int Count
        {
            get
            {
                return form.Count;
            }
        }

        public void Add(string key, string value)
        {
            form.Add(key, value);
        }

        public bool Contains(string name) => form.ContainsKey(name);

        public string Get(string name)
        {
            if (!this.Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return form[name];
        }

        public IEnumerator<string> GetEnumerator()
        {
            return form.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public string this[string name] => form[name];
    }
}
