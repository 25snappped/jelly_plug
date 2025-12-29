(function () {
    const pluginId = "e83b6e61-7f54-4a2d-821a-3d0d1b0f88c7";

    function parseMenuInput(raw) {
        return raw.split(/\r?\n/).map(line => line.trim()).filter(Boolean).map(line => {
            const [label, icon, url, openInNewTab] = line.split('|');
            return {
                label: label || '',
                icon: icon || 'fas fa-link',
                url: url || '#',
                openInNewTab: String(openInNewTab || 'true').toLowerCase() !== 'false'
            };
        });
    }

    function stringifyMenuInput(links) {
        return (links || []).map(l => `${l.label}|${l.icon}|${l.url}|${l.openInNewTab}`).join('\n');
    }

    function loadConfig() {
        Dashboard.showLoadingMsg();
        ApiClient.getPluginConfiguration(pluginId).then(config => {
            document.getElementById('BackgroundImageUrl').value = config.BackgroundImageUrl || '';
            document.getElementById('BackgroundColor').value = config.BackgroundColor || '';
            document.getElementById('LogoUrl').value = config.LogoUrl || '';
            document.getElementById('AccentColor').value = config.AccentColor || '';
            document.getElementById('NeutralColor').value = config.NeutralColor || '';
            document.getElementById('ButtonColor').value = config.ButtonColor || '';
            document.getElementById('FooterHtml').value = config.FooterHtml || '';
            document.getElementById('CustomJavaScript').value = config.CustomJavaScript || '';
            document.getElementById('MenuLinks').value = stringifyMenuInput(config.MenuLinks);
            Dashboard.hideLoadingMsg();
        });
    }

    function saveConfig(e) {
        e.preventDefault();
        Dashboard.showLoadingMsg();

        ApiClient.getPluginConfiguration(pluginId).then(config => {
            config.BackgroundImageUrl = document.getElementById('BackgroundImageUrl').value || null;
            config.BackgroundColor = document.getElementById('BackgroundColor').value || '#0b1221';
            config.LogoUrl = document.getElementById('LogoUrl').value || null;
            config.AccentColor = document.getElementById('AccentColor').value || '#00a4ff';
            config.NeutralColor = document.getElementById('NeutralColor').value || '#e1e5ed';
            config.ButtonColor = document.getElementById('ButtonColor').value || '#12b76a';
            config.FooterHtml = document.getElementById('FooterHtml').value || '';
            config.CustomJavaScript = document.getElementById('CustomJavaScript').value || '';
            config.MenuLinks = parseMenuInput(document.getElementById('MenuLinks').value);

            ApiClient.updatePluginConfiguration(pluginId, config).then(Dashboard.processPluginConfigurationUpdateResult);
        });
    }

    document.addEventListener('DOMContentLoaded', () => {
        document.getElementById('jellyplugConfigurationForm').addEventListener('submit', saveConfig);
    });

    $(document).on('pageshow', '#jellyplugConfigurationPage', loadConfig);
})();
