using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public class HttpCookieCollection : IEnumerable<HttpCookie>
    {
        private readonly Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            cookies = new Dictionary<string, HttpCookie>();
        }

        public int Count
        {
            get
            {
                return cookies.Count;
            }
        }

        public void Add(string name, string value)
        {
            var cookie = new HttpCookie(name, value);

            cookies[cookie.Name] = cookie;
        }

        public bool Contains(string name) => cookies.ContainsKey(name);

        public HttpCookie Get(string name)
        {
            if (!this.Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return cookies[name];
        }

        public HttpCookie this[string name] => cookies[name];

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            return cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
