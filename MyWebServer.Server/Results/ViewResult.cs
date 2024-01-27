
namespace MyWebServer.Server.Results
{
    using MyWebServer.Server.Http;

    public class ViewResult : ActionResult
    {
        private const char PathSeparator = '/';

        public ViewResult(HttpResponse response, string filePath, string controllerName, object model)
            : base(response)
        {
            this.GetHtml(filePath, controllerName, model);
        }

        private void GetHtml(string viewName, string controllerName, object model)
        {
            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }

            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");

            if (!File.Exists(viewPath))
            {
                PrepareMissingViewError(viewPath);
                return;
            }


            var viewContent = File.ReadAllText(viewPath);

            if (model != null)
            {
                viewContent = PopulateModel(viewContent, model);
            }

            var layoutPath = Path.GetFullPath("./Views/Layout.cshtml");

            if (File.Exists(layoutPath))
            {
                var layoutContent = File.ReadAllText(layoutPath);

                const string openingBrackets = "{{";
                const string closingBrackets = "}}";

                viewContent = layoutContent.Replace($"{openingBrackets}RenderBody(){closingBrackets}", viewContent);
            }

            this.SetContent(viewContent, HttpContentType.HtmlText);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;
            var errorMessage = $"View '{viewPath}' was not found.";

            this.SetContent(errorMessage, HttpContentType.PlainText);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model
                .GetType()
                .GetProperties()
                .Select(pr => new
                {
                    Name = pr.Name,
                    Value = pr.GetValue(model)
                });

            const string openingBrackets = "{{";
            const string closingBrackets = "}}";

            foreach (var entry in data)
            {
                var currValue = entry.Value?.ToString();
                viewContent = viewContent.Replace($"{openingBrackets}{entry.Name}{closingBrackets}", currValue);
            }

            return viewContent;
        }
    }
}
