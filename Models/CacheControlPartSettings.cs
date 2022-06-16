namespace Etch.OrchardCore.CacheControl.Models
{
    public class CacheControlPartSettings : ICacheControl
    {
        public string Directive { get; set; }
        public int Duration { get; set; }
        public bool ForcePrivateWhenAuthenticated { get; set; }
    }
}
