namespace MyWebServer.Server.Common
{
    public static class Guard
    {
        public static void AgainsNull(object value, string name = null)
        {
            if (value == null)
            {
                name ??= "Value";

                throw new ArgumentException($"{name} cannot be null.");
            }
        }
    }
}
