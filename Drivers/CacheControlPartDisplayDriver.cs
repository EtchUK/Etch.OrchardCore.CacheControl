using Etch.OrchardCore.CacheControl.Models;
using Etch.OrchardCore.CacheControl.ViewModels;
using Microsoft.Extensions.Localization;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Display.Models;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace Etch.OrchardCore.CacheControl.Drivers
{
    public class CacheControlPartDisplayDriver : ContentPartDisplayDriver<CacheControlPart>
    {
        private readonly IStringLocalizer S;

        public CacheControlPartDisplayDriver(IStringLocalizer<CacheControlPartDisplayDriver> localizer)
        {
            S = localizer;
        }

        public override IDisplayResult Edit(CacheControlPart part, BuildPartEditorContext context)
        {
            var settings = context.TypePartDefinition.GetSettings<CacheControlPartSettings>();

            return Initialize<CacheControlPartEditViewModel>(GetEditorShapeType(context), model =>
            {
                model.Duration = part.UseDefault ? settings.Duration : part.Duration;
                model.Directive = part.UseDefault ? settings.Directive : part.Directive;
                model.UseDefault = part.UseDefault;
            }).Location("Parts:25#Caching");
        }

        public override async Task<IDisplayResult> UpdateAsync(CacheControlPart part, IUpdateModel updater, UpdatePartEditorContext context)
        {
            var model = new CacheControlPartEditViewModel();

            if (!await updater.TryUpdateModelAsync(model, Prefix))
            {
                return Edit(part, context);
            }

            if (!model.UseDefault && string.IsNullOrWhiteSpace(model.Directive))
            {
                updater.ModelState.AddModelError(Prefix, nameof(model.Directive), S["Must define directive when not using default caching behaviour."]);
                return Edit(part, context);
            }

            part.Duration = model.Duration;
            part.Directive = model.Directive;
            part.UseDefault = model.UseDefault;

            return Edit(part);
        }
    }
}
