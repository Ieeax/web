export async function readText(value: string): Promise<string> {

    let permission: PermissionStatus;
    try {
        permission = await navigator.permissions
            .query({ name: <any>"clipboard-read" });
    }
    catch {
        return null;
    }

    if (permission.state == "granted" || permission.state == "prompt") {
        try {
            return await navigator.clipboard.readText();
        }
        catch {
            if (!document.execCommand) {
                return null;
            }

            let pastedText;
            if (createTemporaryTextArea(e => {
                e.focus();
                const result = document.execCommand('paste');
                pastedText = e.value;
                return result;
            }))
            {
                return pastedText;
            }
        }
    }
    
    return null;
}

export async function writeText(value: string): Promise<boolean> {

    try {
        const permission = await navigator.permissions
            .query({ name: <any>"clipboard-write" });

        if (permission && (permission.state == "granted" || permission.state == "prompt")) {
            await navigator.clipboard.writeText(value);
            return true;
        }
    }
    catch {
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
}

function createTemporaryTextArea(action: (e: HTMLTextAreaElement) => boolean): boolean {
    
    const body = document.getElementsByTagName('body')[0];
    const tempInput = document.createElement('textarea');
    tempInput.style.height = "0px !important";
    tempInput.style.maxHeight = "0px !important";
    tempInput.style.width = "0px !important";
    tempInput.style.maxWidth = "0px !important";
    
    // Add textarea to body
    body.appendChild(tempInput);
    
    let result;
    try {
        // Execute action which copies or pastes something into the textarea
        result = action(tempInput);
    }
    catch {
        result = false;
    }
    
    // Remove textarea from body
    body.removeChild(tempInput);
    return result;
}