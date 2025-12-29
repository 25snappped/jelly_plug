using MediaBrowser.Controller.Configuration;
using MediaBrowser.Controller.Plugins;
using MediaBrowser.Model.Branding;
using MediaBrowser.Model.Configuration;

namespace Jellyfin.Plugin.JellyPlug;

public class BrandingBootstrapper : IServerEntryPoint
{
    private readonly IServerConfigurationManager _configurationManager;

    public BrandingBootstrapper(IServerConfigurationManager configurationManager)
    {
        _configurationManager = configurationManager;
    }

    public void Dispose()
    {
    }

    public void Run()
    {
        // Wire our CSS automatically so administrators do not need to edit server branding by hand.
        var config = _configurationManager.Configuration;
        var branding = config.BrandingOptions ?? new BrandingOptions();

        var cssImport = "@import url(\"/JellyPlug/client/custom.css\");";
        if (branding.CustomCss is null || !branding.CustomCss.Contains(cssImport))
        {
            branding.CustomCss = string.IsNullOrWhiteSpace(branding.CustomCss)
                ? cssImport
                : branding.CustomCss + "\n" + cssImport;
            config.BrandingOptions = branding;
            _configurationManager.SaveConfiguration();
        }

        // Add a small bootstrap to load the JS side when the dashboard renders.
        const string loaderSnippet = "<script defer src=\"/JellyPlug/client/custom.js\"></script>";
        if (branding.LoginDisclaimer is null || !branding.LoginDisclaimer.Contains(loaderSnippet))
        {
            branding.LoginDisclaimer = string.IsNullOrWhiteSpace(branding.LoginDisclaimer)
                ? loaderSnippet
                : branding.LoginDisclaimer + "\n" + loaderSnippet;
            config.BrandingOptions = branding;
            _configurationManager.SaveConfiguration();
        }
    }
}
