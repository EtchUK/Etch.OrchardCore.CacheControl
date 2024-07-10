using Etch.OrchardCore.CacheControl.Models;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CacheControl
{
    public class Migrations : DataMigration
    {
        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Migrations(IContentDefinitionManager contentDefinitionManager)
        {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public async Task<int> CreateAsync()
        {
            await _contentDefinitionManager.AlterPartDefinitionAsync(nameof(CacheControlPart), builder => builder
                .Attachable()
                .WithDisplayName("Cache Control")
                .WithDescription("Configure cache-control response header")
            );

            return 1;
        }
    }
}
