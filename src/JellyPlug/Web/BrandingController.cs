using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Jellyfin.Plugin.JellyPlug.Web;

[ApiController]
[Route("JellyPlug")] 
public class BrandingController : ControllerBase
{
    [HttpGet("client/custom.css")]
    public IActionResult GetCustomCss()
    {
        var config = Plugin.Instance.Configuration;
        var css = new StringBuilder();

        css.AppendLine(":root {");
        css.AppendLine($"  --jellyplug-accent: {config.AccentColor};");
        css.AppendLine($"  --jellyplug-neutral: {config.NeutralColor};");
        css.AppendLine($"  --jellyplug-button: {config.ButtonColor};");
        css.AppendLine("}");

        css.AppendLine("body, html { background-color: " + config.BackgroundColor + "; }");

        if (!string.IsNullOrWhiteSpace(config.BackgroundImageUrl))
        {
            css.AppendLine("body::before {");
            css.AppendLine("  content: ''; position: fixed; inset: 0; z-index: -2;");
            css.AppendLine($"  background: url('{config.BackgroundImageUrl}') center/cover no-repeat fixed;");
            css.AppendLine("}");
        }

        if (!string.IsNullOrWhiteSpace(config.LogoUrl))
        {
            css.AppendLine(".logoImage { background-image: url('" + config.LogoUrl + "') !important; background-size: contain; background-repeat: no-repeat; }");
            css.AppendLine(".headerLogo img, .pageTitleWithLogo img { display:none !important; }");
        }

        css.AppendLine(".accentButton, .fab, button.emby-button { background: var(--jellyplug-button); color: #fff; border: none; }");
        css.AppendLine("a, .emby-linkbutton { color: var(--jellyplug-accent); }");
        css.AppendLine(".detailPagePrimaryContent, .dialog, .cardBox { border-color: var(--jellyplug-neutral); }");
        css.AppendLine(".footerContent { font-size: 90%; }");

        if (!string.IsNullOrWhiteSpace(config.FooterHtml))
        {
            css.AppendLine(".footerContent::after { display:block; content:''; }");
        }

        return Content(css.ToString(), "text/css", Encoding.UTF8);
    }

    [HttpGet("client/custom.js")]
    public IActionResult GetCustomJs()
    {
        var config = Plugin.Instance.Configuration;
        var builder = new StringBuilder();
        builder.AppendLine("(() => {");
        var footerHtml = config.FooterHtml ?? string.Empty;
        builder.AppendLine("  const footer = document.querySelector('.footerContent');");
        builder.AppendLine("  if (footer) { footer.insertAdjacentHTML('beforeend', `" + Escape(footerHtml) + "`); }");

        builder.AppendLine("  const menu = document.querySelector('.mainDrawer .navMenu');");
        builder.AppendLine("  if (menu) {");
        builder.AppendLine("    const existing = Array.from(menu.querySelectorAll('li')); existing.forEach(item => item.classList.add('jellyplug-original'));");
        builder.AppendLine("    const links = " + System.Text.Json.JsonSerializer.Serialize(config.MenuLinks) + ";");
        builder.AppendLine("    links.forEach(link => {");
        builder.AppendLine("      const li = document.createElement('li'); li.className = 'navMenuOption';");
        builder.AppendLine("      const a = document.createElement('a'); a.className = 'emby-button navMenuLink'; a.href = link.url; a.setAttribute('is', 'emby-linkbutton'); a.target = link.openInNewTab ? '_blank' : '_self';");
        builder.AppendLine("      a.innerHTML = `<i class='${link.icon}'></i><span>${link.label}</span>`;");
        builder.AppendLine("      li.appendChild(a); menu.appendChild(li);");
        builder.AppendLine("    });");
        builder.AppendLine("  }");

        if (!string.IsNullOrWhiteSpace(config.CustomJavaScript))
        {
            builder.AppendLine(config.CustomJavaScript);
        }

        builder.AppendLine("})();");
        return Content(builder.ToString(), "application/javascript", Encoding.UTF8);
    }

    private static string Escape(string value) => value
        .Replace("`", "\\`")
        .Replace("\\", "\\\\")
        .Replace("$", "\\$");
}
