namespace StoryBooks.Features.Identity.Application;

internal class IdentityApplicationConstants
{
    public class Messages
    {
        public const string UserUpdateSuccess = "Profile details updated.";
        public const string UserUpdateError = "Error on updating your profile details.";

        public const string UserRegistrationError = "Registration is not successful!";
        public const string UserRegistrationSuccess = "Successful registration.";

        public const string InvalidLoginError = "Email or password are invalid!";
        public const string LoggedSuccessfully = "Logged successfully.";

        public const string PasswordChanged = "Your password is changed";
        public const string PasswordChangeError = "Could not change your password!";

        public const string EmailConfirmSent = "Email with confirmation instructions is sent to you.";
        public const string EmailConfirmed = "Your email is confirmed";
        public const string EmailConfirmError = "Error confirming your email!";

        public const string UserNotFoundError = "Profile not found!";

        public const string UserDeletedSuccess = "Your profile is deleted!";
        public const string UserDeletedError = "Could not delete your profile!";
        public const string UserDeletedPasswordError = "Invalid password!";
    }
}