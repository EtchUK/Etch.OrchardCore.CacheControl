using Etch.OrchardCore.CacheControl.Extensions;
using Etch.OrchardCore.CacheControl.Models;
using Etch.OrchardCore.CacheControl.Utils;
using Microsoft.AspNetCore.Http;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CacheControl.Drivers
{
    public class CacheControlDriver : ContentDisplayDriver
    {
        private const string CacheControlResponseHeader = "Cache-Control";

        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private bool _primaryContentRendered { get; set; }

        public CacheControlDriver(IContentDefinitionManager contentDefinitionManager, IHttpContextAccessor httpContextAccessor) 
        {
            _contentDefinitionManager = contentDefinitionManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public override async Task<IDisplayResult> DisplayAsync(ContentItem contentItem, BuildDisplayContext context)
        {
            if (_primaryContentRendered)
            {
                return null;
            }

            _primaryContentRendered = true;

            if (context.DisplayType != "Detail" || 
                context.Shape.TryGetProperty(nameof(ContentTypeSettings.Stereotype), out string _) ||
                !contentItem.Has<CacheControlPart>())
            {
                return null;
            }

            var httpContext = _httpContextAccessor.HttpContext;
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;

            if (!isAuthenticated && contentItem.ModifiedUtc.HasValue) 
            {
                httpContext.Response.Headers["Last-Modified"] = contentItem.ModifiedUtc.Value.ToString("R");

                var ims = httpContext.Request.Headers["If-Modified-Since"];

                if (!string.IsNullOrEmpty(ims) && !CacheControlUtils.IsModifiedSince(contentItem.ModifiedUtc.Value, ims))
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotModified;
                    return null;
                }
            }

            httpContext.Response.Headers[CacheControlResponseHeader] = await GetCacheControlAsync(contentItem.As<CacheControlPart>()).GetCacheControlHeader(isAuthenticated);

            return null;
        }

        private async Task<CacheControlPartSettings> GetDefaultSettingsAsync(ContentItem contentItem)
        {
            var typeDefinition = await _contentDefinitionManager.GetTypeDefinitionAsync(contentItem.ContentType);
            var partDefinition = typeDefinition.Parts.FirstOrDefault(x => x.Name == nameof(CacheControlPart));

            if (partDefinition == null)
            {
                return null;
            }

            return partDefinition.GetSettings<CacheControlPartSettings>();
        }

        private async Task<ICacheControl> GetCacheControlAsync(CacheControlPart cacheControlPart)
        {
            ICacheControl cacheControl = cacheControlPart;
            var defaultSettings = await GetDefaultSettingsAsync(cacheControlPart.ContentItem);

            if (cacheControlPart.UseDefault && defaultSettings != null)
            {
                cacheControl = defaultSettings;
            }

            return cacheControl;
        }
    }
}
