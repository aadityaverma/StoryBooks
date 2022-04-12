namespace StoryBooks.Features.Application;

public class RedirectResult : BaseResult
{
    public RedirectResult(string redirectUrl)
        :base(ResultCode.TemporaryRedirect, redirectUrl)
    {
        this.RedirectUrl = redirectUrl;
    }

    public string RedirectUrl { get; set; }
}