namespace StoryBooks.Libraries.Email.Renderers.FluidEngine
{
    using StoryBooks.Libraries.Validation;

    public class LayoutModel
    {
        public string Content { get; private set; } = default!;

        public string ClientUrl { get; private set; } = default!;

        public string TermsUrl { get; private set; } = default!;

        public string PrivacyUrl { get; private set; } = default!;

        public LayoutModel SetContent(string content)
        {
            this.Content = content;
            return this;
        }

        public LayoutModel SetClientUrl(string clientUrl)
        {
            Guard.ForValidUrl<ValidationException>(clientUrl);
            this.ClientUrl = clientUrl;
            this.PrivacyUrl = $"{clientUrl}/privacy-policy";
            this.TermsUrl = $"{clientUrl}/terms-of-use";
            return this;
        }
    }
}
