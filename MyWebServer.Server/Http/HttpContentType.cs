using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebServer.Server.Http
{
    public class HttpContentType
    {
        public const string PlainText = $"text/plain; charset=UTF-8";
        public const string HtmlText = $"text/html; charset=UTF-8";
        public const string FormUrlEncoded = "application/x-www-form-urlencoded";

        public static string GetFileByExtension(string fileExtension)
            => fileExtension switch
            {
                "css" => $"text/css",
                "js" => $"application/javascript",
                "jpg" or "jpeg" => "image/jpeg",
                "png" => "image/png",
                _ => PlainText
            };
    }
}
