namespace StoryBooks.Libraries.Email.Models
{
    using Microsoft.Extensions.FileProviders;

    using StoryBooks.Libraries.Email.IO;

    public class EmailSettings
    {
        public string SenderApiKey { get; private set; } = default!;

        public string SenderAddress { get; private set; } = default!;

        public string SenderName { get; private set; } = default!;

        /// <summary>
        /// Specific dev related settings
        /// </summary>
        public EmailDevSettings Dev { get; private set; } = new EmailDevSettings();
    }

    public class EmailDevSettings
    {
        /// <summary>
        /// Razor views extension
        /// </summary>
        public string RazorViewExtension { get; private set; }
            = ".cshtml";
        
        /// <summary>
        /// Liquid views extensions
        /// </summary>
        public string FluidViewExtension { get; private set; }
            = ".html";

        /// <summary>
        /// HTML file extension
        /// </summary>
        public string HtmlViewExtension { get; private set; }
            = ".html";

        /// <summary>
        /// Naming convenction for template model class name
        /// </summary>
        public string ModelNameSuffix { get; private set; }
            = "Model";

        /// <summary>
        /// Define file provider that is used form the template renderers
        /// </summary>
        public IFileProvider FileProvider { get; private set; } 
            = new CombinedResourcesFileProvider(AppDomain.CurrentDomain.BaseDirectory);

        public void SetFileProvider(IFileProvider fileProvider)
        {
            this.FileProvider = fileProvider;
        }
    }
}