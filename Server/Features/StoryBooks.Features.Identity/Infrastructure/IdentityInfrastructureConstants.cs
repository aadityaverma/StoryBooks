namespace StoryBooks.Features.Identity.Infrastructure;

public static class IdentityInfrastructureConstants
{
    public static class Messages
    {
        public const string InvalidLoginError = "User or password are invalid!";

        public const string LoggedSuccessfully = "Logged successfully.";

        public const string PasswordChanged = "User password is changed";

        public const string PasswordChangeError = "User password was not changed!";

        public const string UserNotFoundError = "User is not found!";

        public const string UserRegistrationError = "User registration is not successful!";

        public const string UserRegistrationSuccess = "User registration is successful.";
    }

    public static class Email
    {
        public const string UserRegistrationSubject = "Registration confirmation";
    }
}