using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
    public abstract class ActionResult : HttpResponse
    {
        protected ActionResult(
            HttpResponse response
            ) 
            : base(response.StatusCode)
        {
            PrepareHeaders(response.Headers);
            PrepareCookies(response.Cookies);
        }

        private void PrepareHeaders(HttpHeadersCollection headers)
        {
            foreach (var header in headers)
            {
                AddHeader(header.Name, header.Value);
            }
        }

        private void PrepareCookies(HttpCookieCollection cookies)
        {
            foreach (var cookie in cookies)
            {
                AddCookie(cookie.Name, cookie.Value);
            }
        }
    }
}
