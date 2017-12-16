namespace Prestissimo.Web.Infrastructure.Extensions
{
    using System;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
            => Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();

        public static string ToDate(this DateTime dateTime)
            => dateTime.ToShortDateString();
    }
}
