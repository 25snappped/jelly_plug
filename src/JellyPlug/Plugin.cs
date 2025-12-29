using System;
using System.Collections.Generic;
using Jellyfin.Plugin.JellyPlug.Web;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Plugins;

namespace Jellyfin.Plugin.JellyPlug;

public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
{
    public static Plugin Instance { get; private set; } = null!;

    public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
        : base(applicationPaths, xmlSerializer)
    {
        Instance = this;
    }

    public override string Name => "JellyPlug";

    public override string Description => "Brand and customize the Jellyfin web experience with unified assets and simple UI controls.";

    public override Guid Id { get; } = Guid.Parse("e83b6e61-7f54-4a2d-821a-3d0d1b0f88c7");

    public IEnumerable<PluginPageInfo> GetPages()
    {
        return new[]
        {
            new PluginPageInfo
            {
                Name = "jellyplug",
                EmbeddedResourcePath = PluginEmbeds.WebPage
            },
            new PluginPageInfo
            {
                Name = "jellyplug.js",
                EmbeddedResourcePath = PluginEmbeds.WebScript
            },
            new PluginPageInfo
            {
                Name = "jellyplug.css",
                EmbeddedResourcePath = PluginEmbeds.WebStyles
            },
            new PluginPageInfo
            {
                Name = "jellyplug-client.js",
                EmbeddedResourcePath = PluginEmbeds.ClientScript
            }
        };
    }
}
