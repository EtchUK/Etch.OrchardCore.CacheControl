﻿using OrchardCore.ContentManagement;

namespace Etch.OrchardCore.CacheControl.Models
{
    public class CacheControlPart : ContentPart
    {
        public string Directive { get; set; }
        public int Duration { get; set; }
        public bool UseDefault { get; set; } = true;
    }
}
