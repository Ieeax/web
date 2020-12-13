export function setCookieRaw(value) {
    if (typeof value === "string") {
        document.cookie = value;
    }
}
export function getCookie(name) {
    if (typeof name !== "string") {
        return null;
    }
    var cookieArr = document.cookie.split(";");
    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");
        if (name == cookiePair[0].trim()) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
}
export function removeCookie(name) {
    if (typeof name === "string") {
        document.cookie = name + "=; max-age=0";
    }
}
