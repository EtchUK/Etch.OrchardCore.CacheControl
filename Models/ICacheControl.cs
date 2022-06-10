namespace Etch.OrchardCore.CacheControl.Models
{
    public interface ICacheControl
    {
        string Directive { get; set; }
        int Duration { get; set; }
    }
}
