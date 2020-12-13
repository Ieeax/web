export function setTitle(value) {
    document.title = value;
}
export function addOrUpdateStyle(content, type, key) {
    let styleTag;
    if (key) {
        styleTag = document.querySelector("head style[key='" + key + "']");
    }
    const createNewTag = !styleTag;
    if (createNewTag) {
        styleTag = document.createElement("style");
        if (key) {
            styleTag.setAttribute("key", key);
        }
    }
    ensureAttribute(styleTag, "type", type);
    styleTag.innerHTML = content;
    if (createNewTag) {
        const head = document.getElementsByTagName("head")[0];
        head.appendChild(styleTag);
    }
}
export function removeStyle(key) {
    if (key) {
        const styleTag = document.querySelector("head style[key='" + key + "']");
        if (styleTag) {
            styleTag.parentElement.removeChild(styleTag);
            return true;
        }
    }
    return false;
}
export function addOrUpdateLink(options) {
    if (!options || !options.href || !options.rel) {
        return;
    }
    const key = options.key == null
        ? "href='" + options.href + "'"
        : "key='" + options.key + "'";
    let linkTag = document.querySelector("head link[" + key + "]");
    const createNewTag = !linkTag;
    if (createNewTag) {
        linkTag = document.createElement("link");
        if (options.key) {
            linkTag.setAttribute("key", options.key);
        }
    }
    ensureAttribute(linkTag, "crossorigin", options.crossorigin);
    ensureAttribute(linkTag, "href", options.href);
    ensureAttribute(linkTag, "hreflang", options.hreflang);
    ensureAttribute(linkTag, "media", options.media);
    ensureAttribute(linkTag, "referrerpolicy", options.referrerpolicy);
    ensureAttribute(linkTag, "rel", options.rel);
    ensureAttribute(linkTag, "sizes", options.sizes);
    ensureAttribute(linkTag, "title", options.title);
    ensureAttribute(linkTag, "type", options.type);
    if (createNewTag) {
        const head = document.getElementsByTagName("head")[0];
        head.appendChild(linkTag);
    }
}
export function removeLink(key) {
    if (key) {
        let linkTag = document.querySelector("head link[key='" + key + "']");
        if (!linkTag) {
            linkTag = document.querySelector("head link[href='" + key + "']");
        }
        if (linkTag) {
            linkTag.parentElement.removeChild(linkTag);
            return true;
        }
    }
    return false;
}
export function addOrUpdateScript(options) {
    return new Promise((resolve, reject) => {
        if (!options || !options.src) {
            reject("Invalid options.");
            return;
        }
        const key = options.key == null
            ? "src='" + options.src + "'"
            : "key='" + options.key + "'";
        let scriptTag = document.querySelector("head script[" + key + "]");
        const createNewTag = !scriptTag;
        const timeoutMs = isNaN(options.timeoutMs) || options.timeoutMs <= 0
            ? 60000
            : options.timeoutMs;
        if (createNewTag) {
            scriptTag = document.createElement("script");
            if (options.key) {
                scriptTag.setAttribute("key", options.key);
            }
        }
        const onLoadCallback = () => {
            scriptTag.removeEventListener("load", onLoadCallback);
            resolve();
        };
        scriptTag.addEventListener("load", onLoadCallback);
        setTimeout(() => {
            scriptTag.removeEventListener("load", onLoadCallback);
            reject("Timeout of " + timeoutMs + "ms was reached. Script couldn't be loaded.");
        }, timeoutMs);
        ensureAttribute(scriptTag, "async", options.async);
        ensureAttribute(scriptTag, "charset", options.charset);
        ensureAttribute(scriptTag, "defer", options.defer);
        ensureAttribute(scriptTag, "type", options.type);
        ensureAttribute(scriptTag, "src", options.src);
        if (createNewTag) {
            const head = document.getElementsByTagName("head")[0];
            head.appendChild(scriptTag);
        }
    });
}
export function removeScript(key) {
    if (key) {
        let scriptTag = document.querySelector("head script[key='" + key + "']");
        if (!scriptTag) {
            scriptTag = document.querySelector("head script[src='" + key + "']");
        }
        if (scriptTag) {
            scriptTag.parentElement.removeChild(scriptTag);
            return true;
        }
    }
    return false;
}
function ensureAttribute(element, attrName, attrValue) {
    if (!attrValue) {
        if (element.hasAttribute(attrName)) {
            element.removeAttribute(attrName);
        }
        return;
    }
    if (attrValue !== false) {
        element.setAttribute(attrName, attrValue === true ? "" : attrValue);
    }
}
