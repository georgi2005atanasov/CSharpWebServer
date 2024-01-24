using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public class HttpSessionsCollection : IEnumerable<HttpSession>
    {
        private static Dictionary<string, HttpSession> sessions;

        public HttpSessionsCollection()
        {
            sessions = new();
        }

        public int Count
        {
            get
            {
                return sessions.Count;
            }
        }

        public void Add(string id, HttpSession session)
        {
            sessions[id] = session;
        }

        public bool Contains(string name) => sessions.ContainsKey(name);

        public HttpSession Get(string name)
        {
            if (!this.Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return sessions[name];
        }

        public IEnumerator<HttpSession> GetEnumerator()
        {
            return sessions.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public HttpSession this[string name] => sessions[name];

    }
}
