# JellyPlug

A Jellyfin plugin that brands and customizes the web client without patching the upstream UI. JellyPlug injects your own background, logo, accent colors, footer content, custom JavaScript, and menu shortcuts through a single configuration page.

## Features
- Swap the default background and fallback color.
- Replace the default Jellyfin logo with your own asset.
- Override accent, neutral, and primary button colors for existing themes.
- Append footer HTML to the dashboard.
- Inject custom JavaScript for advanced tweaks.
- Add navigation menu entries for quick links.

## Usage
1. Build and install the plugin as you would any other Jellyfin server plugin.
2. Open **Dashboard → Plugins → JellyPlug** and set your branding values.
3. Save the configuration. The plugin automatically imports its generated CSS and JS via the server branding settings.

### Repository manifest
- Host the provided `manifest.json` file somewhere Jellyfin can reach (e.g., GitHub Pages or a static file host).
- Update the `sourceUrl` and `checksum` entries to point at your published plugin zip before adding the repository.
- Add the manifest URL under **Dashboard → Plugins → Repositories** so the server can install and track JellyPlug releases.

### Menu editor format
Each line represents a menu item in the format `Label|Icon class|Url|NewTab`. For example:

```
Docs|fas fa-book|https://docs.example.com|true
Support|fas fa-life-ring|https://help.example.com|false
```

## Endpoints
- `/JellyPlug/client/custom.css` – Generated CSS containing your background, logo, and colors.
- `/JellyPlug/client/custom.js` – Generated JS that injects footer HTML, menu entries, and custom code.
