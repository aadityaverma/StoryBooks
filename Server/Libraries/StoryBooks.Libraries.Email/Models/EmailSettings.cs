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
        /// Relative location of email templates files
        /// </summary>
        public string RazorViewExtension { get; private set; }
            = ".cshtml";
        
        /// <summary>
        /// Relative location of email templates files
        /// </summary>
        public string LiquidViewExtension { get; private set; }
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