namespace MyWebServer.Server
{
    using MyWebServer.Server.Http;
    using MyWebServer.Server.Routing;
    using MyWebServer.Server.Services;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener serverListener;

        private readonly RoutingTable routingTable;
        private readonly ServiceCollection serviceCollection;

        private HttpServer(string ipAddress, int port, IRoutingTable routingTable)
        {
            
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            serverListener = new TcpListener(this.ipAddress, port);

            this.routingTable = (RoutingTable)routingTable;

            this.serviceCollection = new ServiceCollection();
        }

        private HttpServer(int port, IRoutingTable routingTable)
            : this("127.0.0.1", port, routingTable)
        {

        }

        private HttpServer(IRoutingTable routingTable)
            : this(5000, routingTable)
        {

        }

        public static HttpServer WithRoutes(Action<IRoutingTable> routingTableConfiguration)
        {
            var routingTable = new RoutingTable();

            var httpServer = new HttpServer(routingTable);

            routingTableConfiguration(routingTable);

            return httpServer;
        }

        public HttpServer WithServices(Action<IServiceCollection> serviceCollectionConfiguration)
        {
            serviceCollectionConfiguration(this.serviceCollection);

            return this;
        }

        public async Task Start()
        {
            serverListener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();

                _ = Task.Run(async () =>
                {
                    var networkStream = connection.GetStream();

                    var requestText = await ReadRequest(networkStream);

                    try
                    {
                        var request = HttpRequest.Parse(requestText, this.serviceCollection);

                        var response = this.routingTable.ExecuteRequest(request);

                        this.PrepareSession(request, response);

                        this.LogPipeline(requestText, response.ToString());

                        await WriteResponse(networkStream, response);
                    }
                    catch (Exception err)
                    {
                        await HandleError(networkStream, err);
                    }

                    connection.Close();
                });
            }
        }

        private void LogPipeline(string request, string response)
        {
            var separator = new string('-', 50);

            var log = new StringBuilder();
            log.AppendLine();
            log.AppendLine(separator);
            log.AppendLine("REQUEST:");
            log.AppendLine(request);
            log.AppendLine();
            log.AppendLine("RESPONSE:");
            log.AppendLine(response);
            log.AppendLine();

            Console.WriteLine(log.ToString());
        }

        private async Task HandleError(NetworkStream networkStream, Exception err)
        {
            var errorMessage = $"{err.Message} {Environment.NewLine} {err.StackTrace}";

            var errorResponse = HttpResponse.ForError(errorMessage);

            await WriteResponse(networkStream, errorResponse);
        }

        private void PrepareSession(HttpRequest request, HttpResponse response)
        {
            if (request.Session.IsNew)
            {
                response.Cookies.Add(HttpSession.SessionCookieName, request.Session.Id);
                request.Session.IsNew = false;
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var totalBytes = 0;

            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);

                totalBytes += bytesRead;

                if (totalBytes > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large.");
                }

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            while (networkStream.DataAvailable);

            return requestBuilder.ToString();
        }

        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());
            await networkStream.WriteAsync(responseBytes);

            if (response.HasContent)
            {
                await networkStream.WriteAsync(response.Content);
            }
        }
    }
}