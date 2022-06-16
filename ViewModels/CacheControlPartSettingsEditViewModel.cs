namespace Etch.OrchardCore.CacheControl.ViewModels
{
    public class CacheControlPartSettingsEditViewModel
    {
        public string Directive { get; set; }
        public int Duration { get; set; }
        public bool ForcePrivateWhenAuthenticated { get; set; }
    }
}
