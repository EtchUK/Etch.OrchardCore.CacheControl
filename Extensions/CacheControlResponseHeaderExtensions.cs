using Etch.OrchardCore.CacheControl.Models;

namespace Etch.OrchardCore.CacheControl.Extensions
{
    public static class CacheControlResponseHeaderExtensions
    {
        private const int OneMinuteInSeconds = 60;

        public static string GetCacheControlHeader(this ICacheControl cacheControlPart)
        {
            var header = string.Empty;

            if (!string.IsNullOrWhiteSpace(cacheControlPart.Directive))
            {
                header += cacheControlPart.Directive;
            }

            if (cacheControlPart.Directive == Constants.CacheDirectives.Private || cacheControlPart.Directive == Constants.CacheDirectives.Public)
            {
                header += !string.IsNullOrEmpty(header) ? $", max-age={ConvertMinutesToSeconds(cacheControlPart.Duration)}" : $"max-age={ConvertMinutesToSeconds(cacheControlPart.Duration)}";
            }

            return header;
        }

        private static int ConvertMinutesToSeconds(int duration)
        {
            if (duration == 0)
            {
                return 0;
            }

            return duration * OneMinuteInSeconds;
        }
    }
}
