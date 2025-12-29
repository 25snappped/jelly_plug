(function(){
    const cssHref = '/JellyPlug/client/custom.css';
    const jsHref = '/JellyPlug/client/custom.js';

    function ensureLink(href){
        if(!document.querySelector(`link[href="${href}"]`)){
            const link = document.createElement('link');
            link.rel='stylesheet';
            link.href=href;
            document.head.appendChild(link);
        }
    }

    function ensureScript(src){
        if(!document.querySelector(`script[src="${src}"]`)){
            const script = document.createElement('script');
            script.src=src;
            script.defer=true;
            document.head.appendChild(script);
        }
    }

    ensureLink(cssHref);
    ensureScript(jsHref);
})();
