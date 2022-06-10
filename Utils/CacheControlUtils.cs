using System;

namespace Etch.OrchardCore.CacheControl.Utils
{
    public static class CacheControlUtils
    {
        public static bool IsModifiedSince(DateTime contentModifiedUtc, string ifModifiedSince)
        {
            if (!string.IsNullOrEmpty(ifModifiedSince) && DateTime.TryParse(ifModifiedSince, out DateTime headerValue))
            {
                return headerValue.ToUniversalTime() < NormalizeDate(contentModifiedUtc);
            }

            return true;
        }

        private static DateTime NormalizeDate(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Kind);
        }
    }
}
