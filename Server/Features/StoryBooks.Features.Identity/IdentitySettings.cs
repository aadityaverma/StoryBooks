namespace StoryBooks.Features.Identity;

public class IdentitySettings
{
    public const string FeatureName = "Identity";

    public int MinPasswordLength { get; private set; }

    public bool RequireDigit { get; private set; }

    public bool RequireLowercase { get; private set; }

    public bool RequireNonAlphanumeric { get; private set; }

    public bool RequireUppercase { get; private set; }
}