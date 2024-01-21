namespace MyWebServer.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class HttpRequest
    {
        private const string NewLine = "\r\n";

        public HttpMethod Method { get; private set; }

        public string? Path { get; private set; }

        public Dictionary<string, string>? Query { get; private set; }

        public HttpHeadersCollection Headers { get; private set; } = new HttpHeadersCollection();

        public string? Body { get; private set; }

        public static HttpRequest Parse(string request)
        {
            var data = request.Split(NewLine);
            var startLine = data.First().Split(" ");

            var method = ParseMethod(startLine[0]);
            var url = startLine[1];

            var (path, query) = ParseUrl(url);

            var headersCollection = ParseHeaders(data.Skip(1).ToList());
            
            var bodyLines = data.Skip(2 + headersCollection.Count).ToArray();
            var body = string.Join(Environment.NewLine, bodyLines);

            return new HttpRequest()
            {
                Method = method,
                Path = path,
                Body = body,
                Headers = headersCollection,
                Query = query
            };
        }

        private static (string, Dictionary<string, string>) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);

            var path = urlParts[0];
            var query = urlParts.Length == 2 ?
                ParseQuery(urlParts[1]) :
                new Dictionary<string, string>();

            return (path, query);
        }

        private static Dictionary<string, string> ParseQuery(string query)
        {
            return query.Split("&")
                .Select(part => part.Split("="))
                .Where(part => part.Length == 2)
                .ToDictionary(p => p.First().ToLower(), p => p.Last());
        }

        private static HttpHeadersCollection ParseHeaders(List<string> headers)
        {
            var headersCollection = new HttpHeadersCollection();
            int i = 0;

            while (true)
            {
                if (headers[i] == string.Empty)
                {
                    break;
                }

                var indexOfColon = headers[i].IndexOf(':');

                if (indexOfColon < 0)
                {
                    throw new InvalidOperationException("Invalid request.");
                }

                var headerName = headers[i].Substring(0, indexOfColon);
                var headerValue = headers[i].Substring(indexOfColon + 1).Trim();

                headersCollection.Add(headerName, headerValue);
                
                i++;
            }

            return headersCollection;
        }

        private static HttpMethod ParseMethod(string method)
        {
            return method.ToUpper() switch
            {
                "GET" => HttpMethod.Get,
                "POST" => HttpMethod.Post,
                "PUT" => HttpMethod.Put,
                "DELETE" => HttpMethod.Delete,
                _ => throw new InvalidOperationException($"Method {method} not supported.")
            };
        }
    }
}
