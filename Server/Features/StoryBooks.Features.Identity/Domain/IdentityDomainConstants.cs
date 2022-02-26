namespace StoryBooks.Features.Identity.Domain
{
    internal static class IdentityDomainConstants
    {
        public static class Validation
        {
            public const int MinNameLength = 2;
            public const int MaxNameLength = 30;

            public const int MaxGuidLength = 38;
        }

        public static class ErrorMessages
        {
            public const string InvalidUserFields = "Email, first and last name are required fields.";

            public const string ConfirmPasswordNotMatching = "'Confirm Password' must be equal to the 'Password' field.";
        }
    }
}
