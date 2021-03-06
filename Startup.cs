using Etch.OrchardCore.CacheControl.Drivers;
using Etch.OrchardCore.CacheControl.Models;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.Data.Migration;
using OrchardCore.Modules;

namespace Etch.OrchardCore.CacheControl
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddContentPart<CacheControlPart>()
                .UseDisplayDriver<CacheControlPartDisplayDriver>();

            services.AddScoped<IContentTypePartDefinitionDisplayDriver, CacheControlPartSettingsDisplayDriver>();
            services.AddScoped<IContentDisplayDriver, CacheControlDriver>();

            services.AddScoped<IDataMigration, Migrations>();
        }
    }
}
