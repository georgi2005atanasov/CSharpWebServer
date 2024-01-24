using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public class HttpSession
    {
        public const string SessionCookieName = "MyWebServerSID";

        public string Id { get; set; }

        private Dictionary<string, string> data { get; }

        public HttpSession(string id)
        {
            data = new();
            Id = id;
        }

        public bool ContainsKey(string key) => data.ContainsKey(key);

        public string this[string key]
        {
            get => data[key];
            set => data[key] = value;
        }
    }
}
