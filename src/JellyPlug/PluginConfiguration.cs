using MediaBrowser.Model.Plugins;
using System.Collections.Generic;

namespace Jellyfin.Plugin.JellyPlug;

public class PluginConfiguration : BasePluginConfiguration
{
    public string? BackgroundImageUrl { get; set; }

    public string BackgroundColor { get; set; } = "#0b1221";

    public string? LogoUrl { get; set; }

    public string AccentColor { get; set; } = "#00a4ff";

    public string NeutralColor { get; set; } = "#e1e5ed";

    public string ButtonColor { get; set; } = "#12b76a";

    public string FooterHtml { get; set; } = string.Empty;

    public string CustomJavaScript { get; set; } = string.Empty;

    public List<MenuLink> MenuLinks { get; set; } = new();
}

public class MenuLink
{
    public string Label { get; set; } = string.Empty;

    public string Icon { get; set; } = "fas fa-link";

    public string Url { get; set; } = string.Empty;

    public bool OpenInNewTab { get; set; } = true;
}
