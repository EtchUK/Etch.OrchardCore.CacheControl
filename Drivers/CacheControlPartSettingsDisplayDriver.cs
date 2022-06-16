using Etch.OrchardCore.CacheControl.Models;
using Etch.OrchardCore.CacheControl.ViewModels;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CacheControl.Drivers
{
    public class CacheControlPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver<CacheControlPart>
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            return Initialize<CacheControlPartSettingsEditViewModel>($"{nameof(CacheControlPartSettings)}_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<CacheControlPartSettings>();

                model.Directive = settings.Directive;
                model.Duration = settings.Duration;
                model.ForcePrivateWhenAuthenticated = settings.ForcePrivateWhenAuthenticated;
            }).Location("Content");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            var model = new CacheControlPartSettingsEditViewModel();
            var settings = new CacheControlPartSettings();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                settings.Directive = model.Directive;
                settings.Duration = model.Duration;
                settings.ForcePrivateWhenAuthenticated = model.ForcePrivateWhenAuthenticated;

                context.Builder.WithSettings(settings);
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}
