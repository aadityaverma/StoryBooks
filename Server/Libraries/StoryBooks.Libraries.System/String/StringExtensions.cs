namespace System;

using System.Text.RegularExpressions;

public static class StringExtensions
{
    private static readonly Regex htmlTagsRegex =
        new(@"</?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)/?>", RegexOptions.Compiled);

    public static string StripHtmlTags(this string html)
    {
        return htmlTagsRegex.Replace(html, string.Empty);
    }

}