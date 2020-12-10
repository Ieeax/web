
export function setCookieRaw(value: string): void {
    if (typeof value === "string") {
        document.cookie = value;
    }
}

export function getCookie(name: string): string {

    if (typeof name !== "string") {
        return null;
    }

    var cookieArr = document.cookie.split(";");

    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");

        // Removing whitespace at the beginning of the cookie before comparing it
        if (name == cookiePair[0].trim()) {
            // Decode the cookie value and return
            return decodeURIComponent(cookiePair[1]);
        }
    }

    return null;
}

export function removeCookie(name: string): void {
    if (typeof name === "string") {
        document.cookie = name + "=; max-age=0";
    }
}