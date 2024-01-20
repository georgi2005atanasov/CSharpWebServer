namespace MyWebServer.Server.Http
{
    using MyWebServer.Server.Common;
    public class HttpHeader
    {
        public string Name { get; init; }
        public string Value { get; init; }

        public HttpHeader(string name, string value)
        {
            Guard.AgainsNull(name, nameof(name));
            Guard.AgainsNull(value, nameof(value));

            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}
