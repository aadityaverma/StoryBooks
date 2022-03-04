namespace StoryBooks.Libraries.Email.Renderers
{
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;

    using StoryBooks.Libraries.Email.Models;
    using StoryBooks.Libraries.Email.Services;

    public class FluidTemplateRenderer : ITemplateRenderer
    {
        private readonly EmailSettings settings;
        private readonly IFileProvider fileProvider;

        public FluidTemplateRenderer(IOptions<EmailSettings> opts)
        {
            this.settings = opts.Value;
            this.fileProvider = settings.Dev.FileProvider;
        }

        public string Extension => settings.Dev.HtmlViewExtension;

        public Task<string> RenderAsync<TModel>(string viewName, TModel model)
        {
            throw new NotImplementedException();
            //var templateInfo = this.fileProvider.GetFileInfo(viewName);
            //using var s = templateInfo.CreateReadStream();
            //using var writer = new StreamReader(s);
        }
    }
}
