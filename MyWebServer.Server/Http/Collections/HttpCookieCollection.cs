namespace MyWebServer.Server.Http.Collections
{
    using MyWebServer.Server.Common;
    using System;
    using System.Collections;

    public class HttpCookieCollection : IEnumerable<HttpCookie>
    {
        private readonly Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        => cookies = new Dictionary<string, HttpCookie>();


        public int Count
        {
            get => cookies.Count;
        }

        public void Add(string name, string value)
        {
            Guard.AgainsNull(name, nameof(name));
            Guard.AgainsNull(value, nameof(value));

            var cookie = new HttpCookie(name, value);

            cookies[cookie.Name] = cookie;
        }

        public bool Contains(string name) => cookies.ContainsKey(name);

        public HttpCookie Get(string name)
        {
            if (!Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return cookies[name];
        }

        public HttpCookie this[string name]
            => cookies[name];

        public IEnumerator<HttpCookie> GetEnumerator()
            => cookies.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
