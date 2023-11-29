# Etch.OrchardCore.CacheControl

Orchard Core module for controlling `cache-control` response headers on content items.

## Build Status

[![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.CacheControl.svg)](https://www.nuget.org/packages/Etch.OrchardCore.CacheControl)

## Orchard Core Reference

This module is referencing a stable build of Orchard Core ([`1.7.2`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.7.2)).

## Installing

This module is available on [NuGet](https://www.nuget.org/packages/Etch.OrchardCore.CacheControl). Add a reference to your Orchard Core web project via the NuGet package manager. Search for "Etch.OrchardCore.CacheControl", ensuring include prereleases is checked.

Alternatively you can [download the source](https://github.com/etchuk/Etch.OrchardCore.CacheControl/archive/main.zip) or clone the repository to your local machine. Add the project to your solution that contains an Orchard Core project and add a reference to Etch.OrchardCore.CacheControl.

## Usage

First step is to enable "Cache Control" within the features section of the admin dashboard. Enabling the module will make a new "Cache Control" part that can be attached to a content type. Once attached, ensure to edit the part settings in order to define the default caching behaviour. Content items by default will use the default but can be overridden via the "Cache" tab when editing a content item.

## Packaging

When the module is compiled (using `dotnet build`) it's configured to generate a `.nupkg` file (this can be found in `\bin\Debug\` or `\bin\Release`).

## Notes

This module was created using `v1.2.0` of [Etch.OrchardCore.ModuleBoilerplate](https://github.com/EtchUK/Etch.OrchardCore.ModuleBoilerplate) template.
