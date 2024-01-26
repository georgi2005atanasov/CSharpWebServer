﻿using System;
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

        public bool IsNew { get; set; }

        public HttpSession(string id)
        {
            data = new();
            Id = id;
        }

        public bool ContainsKey(string key) => data.ContainsKey(key);

        public void Remove(string key)
        {
            if (this.data.ContainsKey(key))
            {
                this.data.Remove(key);
            }
        }

        public string this[string key]
        {
            get => data[key];
            set => data[key] = value;
        }
    }
}
