namespace StoryBooks.Libraries.Email.Renderers.RazrorEngine;

//using Microsoft.Extensions.FileProviders;

//using RazorLight.Razor;

//using StoryBooks.Libraries.Email.Exceptions;

//public class CustomEmbededResourcesProject : RazorLightProject
//{
//    private readonly IFileProvider fileProvider;

//    public CustomEmbededResourcesProject(IFileProvider fileProvider)
//    {
//        this.fileProvider = fileProvider;
//    }

//    public override Task<IEnumerable<RazorLightProjectItem>> GetImportsAsync(string templateKey)
//    {
//        return Task.FromResult(Enumerable.Empty<RazorLightProjectItem>());
//    }

//    public override async Task<RazorLightProjectItem> GetItemAsync(string templateKey)
//    {
//        var item = fileProvider.GetFileInfo(templateKey);
//        if (!item.Exists)
//        {
//            throw new TemplateNotFoundException(templateKey);
//        }

//        using var s = item.CreateReadStream();
//        using var sr = new StreamReader(item.CreateReadStream());

//        string content  = await sr.ReadToEndAsync();
//        return new TextSourceRazorProjectItem(templateKey, content);
//    }
//}