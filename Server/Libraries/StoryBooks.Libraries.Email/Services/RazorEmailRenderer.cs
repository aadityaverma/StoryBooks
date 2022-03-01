namespace StoryBooks.Libraries.Email.Services
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.Razor;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewEngines;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Caching.Memory;

    using StoryBooks.Libraries.Email.Exceptions;

    public class RazorEmailRenderer : IEmailRenderer
    {
        private readonly IRazorViewEngine viewEngine;
        private readonly ITempDataProvider tempDataProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly IMemoryCache viewsCache;

        public RazorEmailRenderer(
            IRazorViewEngine viewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IMemoryCache memoryCache)
        {
            this.viewEngine = viewEngine;
            this.tempDataProvider = tempDataProvider;
            this.serviceProvider = serviceProvider;
            this.viewsCache = memoryCache;
        }

        public async Task<string> RenderAsync<TModel>(string viewPath, TModel model)
        {
            var actionContext = GetActionContext();
            var view = FindView(actionContext, viewPath);

            await using var output = new StringWriter();
            var viewContext = new ViewContext(
                actionContext,
                view,
                new ViewDataDictionary<TModel>(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: new ModelStateDictionary())
                {
                    Model = model
                },
                new TempDataDictionary(
                    actionContext.HttpContext,
                    this.tempDataProvider),
                output,
                new HtmlHelperOptions());

            await view.RenderAsync(viewContext);

            return output.ToString();
        }

        private IView FindView(ActionContext actionContext, string viewName)
        {
            string key = $"RazorViews{viewName}";
            IView? view = this.viewsCache.Get(key) as IView;
            if (view is not null)
            {
                return view;
            }

            var getViewResult = this.viewEngine.GetView(null, viewName, isMainPage: true);
            if (getViewResult.Success)
            {
                view = getViewResult.View;
            }

            var findViewResult = this.viewEngine.FindView(actionContext, viewName, isMainPage: true);
            if (findViewResult.Success)
            {
                view = findViewResult.View;
            }

            if (view is not null)
            {
                this.viewsCache.Set(key, view);
                return view;
            }

            List<string> searchedLocations = new()
            {
                $"Unable to find view '{viewName}'. Searched:"
            };
            searchedLocations.AddRange(findViewResult.SearchedLocations);
            searchedLocations.AddRange(getViewResult.SearchedLocations);
            var errorMessage = string.Join(
                Environment.NewLine,
                searchedLocations);

            throw new EmailTemplateNotFoundException(errorMessage);
        }

        private ActionContext GetActionContext()
        {
            var httpContext = new DefaultHttpContext
            {
                RequestServices = this.serviceProvider
            };
            return new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
        }
    }
}
