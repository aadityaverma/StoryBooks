namespace StoryBooks.Libraries.Validation
{
    public static class CommonValidationConstants
    {
        public const int Zero = 0;
        public const int MaxGuidLength = 38;

        public static class Web
        {
            public const int MaxUrlLength = 2048;
        }

        public static class Phone
        {
            public const int MinLength = 5;
            public const int MaxLength = 20;
            public const string RegularExpression = @"\+[0-9]*";
            public const string FormatErrorMessage = "Phone number must start with a '+' and contain only digits afterwards.";
        }

        public static class Email
        {
            public const int MinLength = 3;
            public const int MaxLength = 250;
            public const string RegularExpression = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        }
    }
}
