namespace StoryBooks.Libraries.Validation;

using System;
using System.Text.RegularExpressions;

using static StoryBooks.Libraries.Validation.CommonValidationConstants;

public static class Guard
{
    public static void ForNull<TValue, TException>(TValue value, string? message = null, string name = "Value")
        where TException : ValidationException, new()
        where TValue : class
    {
        if (value is not null)
        {
            return;
        }

        string msg = message ?? $"{name} cannot be null.";
        ThrowException<TException>(msg);
    }

    public static void ForEmptyString<TException>(string value, string? message = null, string name = "Value")
        where TException : ValidationException, new()
    {
        if (!string.IsNullOrEmpty(value))
        {
            return;
        }

        string msg = message ?? $"{name} cannot be null ot empty.";
        ThrowException<TException>(msg);
    }

    public static void ForMinStringLength<TException>(string value, int minLength, string name = "Value")
        where TException : ValidationException, new()
    {
        ForEmptyString<TException>(value, name: name);
        if (minLength <= value.Length)
        {
            return;
        }

        string msg = $"{name} must contain at least {minLength} symbols.";
        ThrowException<TException>(msg);
    }

    public static void ForMaxStringLength<TException>(string value, int maxLength, string name = "Value")
        where TException : ValidationException, new()
    {
        ForEmptyString<TException>(value, name: name);
        if (maxLength >= value.Length)
        {
            return;
        }

        ThrowException<TException>($"{name} must contain less than {maxLength + 1} symbols.");
    }

    public static void ForStringLength<TException>(string value, int minLength, int maxLength, string name = "Value")
        where TException : ValidationException, new()
    {
        ForEmptyString<TException>(value, name: name);
        if (minLength <= value.Length && value.Length <= maxLength)
        {
            return;
        }

        ThrowException<TException>($"{name} must have between {minLength} and {maxLength} symbols.");
    }

    public static void ForOutOfRange<TException>(int number, int min, int max, string name = "Value")
        where TException : ValidationException, new()
    {
        if (min <= number && number <= max)
        {
            return;
        }

        ThrowException<TException>($"{name} must be between {min} and {max}.");
    }

    public static void ForMinRange<TException>(decimal number, decimal min, string name = "Value")
        where TException : ValidationException, new()
    {
        if (min <= number)
        {
            return;
        }

        ThrowException<TException>($"{name} must be {min} or greater.");
    }

    public static void ForMaxRange<TException>(decimal number, decimal max, string name = "Value")
        where TException : ValidationException, new()
    {
        if (number <= max)
        {
            return;
        }

        ThrowException<TException>($"{name} must be {max} or smaller.");
    }

    public static void ForOutOfRange<TException>(decimal number, decimal min, decimal max, string name = "Value")
        where TException : ValidationException, new()
    {
        if (min <= number && number <= max)
        {
            return;
        }

        ThrowException<TException>($"{name} must be between {min} and {max}.");
    }

    public static void ForValidUrl<TException>(string url, string name = "Value")
        where TException : ValidationException, new()
    {
        if (url.Length <= 2048 &&
            Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            return;
        }

        ThrowException<TException>($"Value for '{name}' must be a valid URL.");
    }

    public static void ForValidFormat<TException>(string value, Regex expression, string? message, string name = "Value")
        where TException : ValidationException, new()
    {
        ForEmptyString<TException>(value, message, name);
        if (expression.IsMatch(value))
        {
            return;
        }

        string msg = message ?? $"Value for '{name}' has incorrect format.";
        ThrowException<TException>(msg);
    }

    public static void ForValidDateOfBirth<TException>(DateTime dateOfBirth, string? message, string name = "Date of birth")
        where TException : ValidationException, new()
    {
        int age = dateOfBirth.UtcAge();
        if (age is < MaxAge and > 0)
        {
            return;
        }

        string msg = message ?? $"'{name}' has incorrect format.";
        ThrowException<TException>(msg);
    }

    private static void ThrowException<TException>(string message)
        where TException : ValidationException, new()
    {
        var exception = new TException
        {
            Error = message
        };

        throw exception;
    }
}