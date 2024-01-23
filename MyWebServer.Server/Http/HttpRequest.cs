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

        public HttpQueryCollection? Query { get; private set; }

        public HttpFormCollection? Form { get; private set; }

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

            var form = ParseForm(headersCollection, body);

            return new HttpRequest()
            {
                Method = method,
                Path = path,
                Body = body,
                Headers = headersCollection,
                Query = query,
                Form = form
            };
        }

        private static HttpFormCollection ParseForm(HttpHeadersCollection headersCollection, string body)
        {
            var result = new HttpFormCollection();

            if (headersCollection.Contains(HttpHeader.ContentType)
                && headersCollection[HttpHeader.ContentType].Value == HttpContentType.FormUrlEncoded)
            {
                var queryData = GetQueryData(body);

                foreach (var queryPart in queryData)
                {
                    result.Add(queryPart[0], queryPart[1]);
                }
            }

            return result;
        }

        private static (string, HttpQueryCollection) ParseUrl(string url)
        {
            var urlParts = url.Split('?', 2);

            var path = urlParts[0];
            var query = urlParts.Length == 2 ?
                ParseQuery(urlParts[1]) :
                new HttpQueryCollection();

            return (path, query);
        }

        private static HttpQueryCollection ParseQuery(string query)
        {
            var queryCollection = new HttpQueryCollection();

            var queryData = GetQueryData(query);

            foreach (var queryPart in queryData)
            {
                queryCollection.Add(queryPart[0], queryPart[1]);
            }

            return queryCollection;
        }

        private static IEnumerable<string[]> GetQueryData(string query)
            => query.Split("&")
                .Select(part => part.Split("="))
                .Where(part => part.Length == 2)
                .ToList();

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
