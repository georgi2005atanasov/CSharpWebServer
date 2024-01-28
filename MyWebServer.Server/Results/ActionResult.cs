using MyWebServer.Server.Http.Collections;

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
                this.Headers.Add(header.Name, header.Value);
            }
        }

        private void PrepareCookies(HttpCookieCollection cookies)
        {
            foreach (var cookie in cookies)
            {
                this.Cookies.Add(cookie.Name, cookie.Value);
            }
        }
    }
}
