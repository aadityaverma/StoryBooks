namespace StoryBooks.Libraries.Email.Renderers.RazrorEngine;

//using Microsoft.Extensions.Options;

//using RazorLight;

//using StoryBooks.Libraries.Email.Models;
//using StoryBooks.Libraries.Email.Services;

//public class RazorTemlateRenderer : ITemplateRenderer
//{
//    private readonly EmailSettings settings;
//    private readonly RazorLightEngine razorEngine;

//    public RazorTemlateRenderer(IOptions<EmailSettings> opts)
//    {
//        this.settings = opts.Value;
//        this.razorEngine = new RazorLightEngineBuilder()
//            .UseProject(new CustomEmbededResourcesProject(settings.Dev.FileProvider))
//            .UseMemoryCachingProvider()
//            .Build();
//    }

//    public string Extension => settings.Dev.RazorViewExtension;

//    public Task<string> RenderAsync<TModel>(string templateName, TModel model)
//    {
//        dynamic? viewBag = (model as IViewBag)?.ViewBag;
//        return razorEngine.CompileRenderAsync(templateName, model, viewBag);
//    }
//}