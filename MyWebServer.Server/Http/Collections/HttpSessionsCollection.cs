
namespace MyWebServer.Server.Http.Collections
{
    using MyWebServer.Server.Common;
    using System.Collections;

    public class HttpSessionsCollection : IEnumerable<HttpSession>
    {
        private static Dictionary<string, HttpSession> sessions;

        public HttpSessionsCollection()
            => sessions = new();

        public int Count
        {
            get => sessions.Count;
        }

        public void Add(string id, HttpSession session)
        {
            Guard.AgainsNull(id, nameof(id));
            Guard.AgainsNull(session, nameof(session));

            sessions[id] = session;
        } 

        public bool Contains(string name) => sessions.ContainsKey(name);

        public HttpSession Get(string name)
        {
            if (!Contains(name))
            {
                throw new InvalidOperationException($"Header with name '{name}' could not be found.");
            }

            return sessions[name];
        }

        public IEnumerator<HttpSession> GetEnumerator()
            => sessions.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public HttpSession this[string name] => sessions[name];

    }
}
