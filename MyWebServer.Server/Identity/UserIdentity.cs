using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Identity
{
    public class UserIdentity
    {
        public string? Id { get; init; }

        public bool IsAuthenticated => this.Id != null;
    }
}
