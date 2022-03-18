namespace System;

using System.Text.RegularExpressions;

public static class UrlExtensions
{
    /// <summary>
    /// Get URL friendly representation of given string
    /// </summary>
    /// <param name="val">Value to be trasnformed</param>
    /// <returns>URL friendly version of the value</returns>
    public static string ToFriendlyUrlAlias(this string val)
    {
        return Regex.Replace(val, @"[^A-Za-z0-9_\.~]+", "-").ToLowerInvariant();
    }
}