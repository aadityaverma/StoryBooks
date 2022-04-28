namespace System;

public static class AgeExtensions 
{
    public static int Age(this DateOnly dateOfBirth)
        => CalculateAge(dateOfBirth, ZoneDateTime.Instance.Date());

    public static int UtcAge(this DateOnly dateOfBirth)
        => CalculateAge(dateOfBirth, UtcDateTime.Instance.Date());

    public static int Age(this DateTime dateOfBirth) 
        => CalculateAge(DateOnly.FromDateTime(dateOfBirth), ZoneDateTime.Instance.Date());

    public static int UtcAge(this DateTime dateOfBirth)
        => CalculateAge(DateOnly.FromDateTime(dateOfBirth), UtcDateTime.Instance.Date());

    static int CalculateAge(DateOnly birth, DateOnly now) 
    {
        int year = now.Year - birth.Year; 
        if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
        {
            year--;
        }

        return year; 
    }
}
