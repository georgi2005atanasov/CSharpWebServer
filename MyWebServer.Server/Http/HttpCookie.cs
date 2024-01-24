using MyWebServer.Server.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public class HttpCookie
    {
        public string Name { get; init; }
        public string Value { get; init; }

        public HttpCookie(string name, string value)
        {
            Guard.AgainsNull(name, nameof(name));
            Guard.AgainsNull(value, nameof(value));

            Name = name;
            Value = value;
        }

        public override string ToString()
             => $"{this.Name}={this.Value}";
    }
}
