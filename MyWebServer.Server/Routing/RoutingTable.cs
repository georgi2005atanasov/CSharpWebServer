namespace MyWebServer.Server.Routing
{
    using MyWebServer.Server.Common;
    using MyWebServer.Server.Http;
    using System;

    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest, HttpResponse>>> routes;

        public RoutingTable() => this.routes = new()
        {
            [HttpMethod.Get] = new(),
            [HttpMethod.Post] = new(),
            [HttpMethod.Put] = new(),
            [HttpMethod.Delete] = new()
        };

        public IRoutingTable Map(
            HttpMethod method,
            string path,
            HttpResponse response)
        {
            Guard.AgainsNull(response, nameof(response));

            return this.Map(method, path, request => response);
        }

        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunction)
        {
            Guard.AgainsNull(path, nameof(path));
            Guard.AgainsNull(responseFunction, nameof(responseFunction));
            Guard.AgainsNull(method, nameof(method));

            this.routes[method][path] = responseFunction;

            return this;
        }

        public IRoutingTable MapGet(
            string path,
            HttpResponse response)
        => this.MapGet(path, request => response);

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunction)
        => Map(HttpMethod.Get, path, responseFunction);

        public IRoutingTable MapPost(
            string path,
            HttpResponse response)
        => this.MapPost(path, request => response);

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunction)
        => Map(HttpMethod.Post, path, responseFunction);

        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestPath = request.Path;

            if (!routes.ContainsKey(requestMethod)
                || !routes[requestMethod].ContainsKey(requestPath))
            {
                return new HttpResponse(HttpStatusCode.NotFound);
            }

            return routes[requestMethod][requestPath](request);
        }
    }
}
