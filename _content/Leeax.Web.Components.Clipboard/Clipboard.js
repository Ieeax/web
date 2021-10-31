var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
export function readText(value) {
    return __awaiter(this, void 0, void 0, function* () {
        let permission;
        try {
            permission = yield navigator.permissions
                .query({ name: "clipboard-read" });
        }
        catch (_a) {
            return null;
        }
        if (permission.state == "granted" || permission.state == "prompt") {
            try {
                return yield navigator.clipboard.readText();
            }
            catch (_b) {
                if (!document.execCommand) {
                    return null;
                }
                let pastedText;
                if (createTemporaryTextArea(e => {
                    e.focus();
                    const result = document.execCommand('paste');
                    pastedText = e.value;
                    return result;
                })) {
                    return pastedText;
                }
            }
        }
        return null;
    });
}
export function writeText(value) {
    return __awaiter(this, void 0, void 0, function* () {
        try {
            const permission = yield navigator.permissions
                .query({ name: "clipboard-write" });
            if (permission && (permission.state == "granted" || permission.state == "prompt")) {
                yield navigator.clipboard.writeText(value);
                return true;
            }
        }
        catch (_a) {
            if (!document.execCommand) {
                return false;
            }
            return createTemporaryTextArea(e => {
                e.value = value;
                e.select();
                return document.execCommand('copy');
            });
        }
        return false;
    });
}
function createTemporaryTextArea(action) {
    const body = document.getElementsByTagName('body')[0];
    const tempInput = document.createElement('textarea');
    tempInput.style.height = "0px !important";
    tempInput.style.maxHeight = "0px !important";
    tempInput.style.width = "0px !important";
    tempInput.style.maxWidth = "0px !important";
    body.appendChild(tempInput);
    let result;
    try {
        result = action(tempInput);
    }
    catch (_a) {
        result = false;
    }
    body.removeChild(tempInput);
    return result;
}
