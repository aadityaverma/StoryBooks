namespace StoryBooks.Features.Identity.Domain
{
    public static class IdentityDomainConstants
    {
        public static class Validation
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 30;

            public const int MaxGuidLength = 38;

            public const string InvalidUserFieldsErrorMessage =
                "Email, first and last name are required fields.";
        }
    }
}
