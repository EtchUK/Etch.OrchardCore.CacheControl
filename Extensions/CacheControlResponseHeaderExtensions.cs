using Etch.OrchardCore.CacheControl.Models;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CacheControl.Extensions
{
    public static class CacheControlResponseHeaderExtensions
    {
        private const int OneMinuteInSeconds = 60;

        public static string GetCacheControlHeader(this ICacheControl cacheControlPart, bool isAuthenticated)
        {
            var header = string.Empty;
            var directive = cacheControlPart.Directive;

            if (!string.IsNullOrWhiteSpace(directive))
            {
                if (isAuthenticated && directive == Constants.CacheDirectives.Public)
                {
                    directive = Constants.CacheDirectives.Private;
                }

                header += directive;
            }

            if (directive == Constants.CacheDirectives.Private || directive == Constants.CacheDirectives.Public)
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
