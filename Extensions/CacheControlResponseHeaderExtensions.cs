using Etch.OrchardCore.CacheControl.Models;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CacheControl.Extensions
{
    public static class CacheControlResponseHeaderExtensions
    {
        private const int OneMinuteInSeconds = 60;

        public static async Task<string> GetCacheControlHeader(this Task<ICacheControl> cacheControlPart, bool isAuthenticated)
        {
            var header = string.Empty;
            var ccp = await cacheControlPart;
            var directive = ccp.Directive;

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
                header += !string.IsNullOrEmpty(header) ? $", max-age={ConvertMinutesToSeconds(ccp.Duration)}" : $"max-age={ConvertMinutesToSeconds(ccp.Duration)}";
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
