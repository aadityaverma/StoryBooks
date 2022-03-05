namespace StoryBooks.Libraries.Email.Renderers
{
    using Fluid;

    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.FileProviders;
    using Microsoft.Extensions.Options;

    using StoryBooks.Libraries.Email.Exceptions;
    using StoryBooks.Libraries.Email.Models;
    using StoryBooks.Libraries.Email.Services;

    public class FluidTemplateRenderer : ITemplateRenderer
    {
        private readonly EmailSettings settings;
        private readonly IFileProvider fileProvider;
        private readonly IMemoryCache cache;
        private readonly FluidParser parser;

        public FluidTemplateRenderer(
            IOptions<EmailSettings> opts,
            IMemoryCache cache,
            FluidParser parser)
        {
            this.settings = opts.Value;
            this.fileProvider = settings.Dev.FileProvider;
            this.cache = cache;
            this.parser = parser;
        }

        public string Extension => settings.Dev.FluidViewExtension;
        
        public async Task<string> RenderAsync<TModel>(string viewName, TModel model)
        {
            var cachedTemplate = this.cache.Get(viewName) as IFluidTemplate;
            if (cachedTemplate is null)
            {
                var templateFile = this.fileProvider.GetFileInfo(viewName);
                if (!templateFile.Exists)
                {
                    throw new TemplateNotFoundException($"Template {viewName} is not found!");
                }

                using var s = templateFile.CreateReadStream();
                using var writer = new StreamReader(s);

                var template = writer.ReadToEnd();
                if (!this.parser.TryParse(template, out cachedTemplate, out var error))
                {
                    throw new TemplateParseException(error);
                }

                this.cache.Set(viewName, cachedTemplate);
            }

            var context = new TemplateContext(model);
            var content = await cachedTemplate.RenderAsync(context);
            if (content is null)
            {
                throw new TemplateRenderException("Content is null!");
            }

            return content ?? string.Empty;
        }
    }
}
