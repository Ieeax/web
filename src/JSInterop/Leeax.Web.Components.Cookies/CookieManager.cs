﻿using Leeax.Web.Components.Abstractions;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Cookies
{
    public class CookieManager : ICookieManager
    {
        public const string ModuleKey = "__" + nameof(CookieManager);
        public const string ModulePath = "./_content/Leeax.Web.Components.Cookies/CookieManager.min.js";

        private readonly IJSInProcessObjectReference? _jsInProcessObjectRef;
        private readonly IJSObjectReferenceStore _jsRefStore;

        public CookieManager(IJSObjectReferenceStore jsRefStore)
        {
            _jsRefStore = jsRefStore;
            jsRefStore.TryGet(ModuleKey, out _jsInProcessObjectRef);
        }

        public string? GetCookie(string name)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            return _jsInProcessObjectRef!.Invoke<string>(
                "getCookie",
                name);
        }

        public async ValueTask<string?> GetCookieAsync(string name)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            return await module.InvokeAsync<string>(
                "getCookie",
                name);
        }

        public void SetCookie(CookieOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            if (!options.TryGetCookieString(out var cookieStr))
            {
                throw new ApplicationException($"The passed {nameof(CookieOptions)} were invalid. The cookie cannot be set.");
            }

            _jsInProcessObjectRef!.InvokeVoid(
                "setCookieRaw",
                cookieStr);
        }

        public async ValueTask SetCookieAsync(CookieOptions options)
        {
            _ = options ?? throw new ArgumentNullException(nameof(options));

            if (!options.TryGetCookieString(out var cookieStr))
            {
                throw new ApplicationException($"The passed {nameof(CookieOptions)} were invalid. The cookie cannot be set.");
            }

            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            await module.InvokeVoidAsync(
                "setCookieRaw",
                cookieStr);
        }

        public void RemoveCookie(string name)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));

            _jsInProcessObjectRef!.InvokeVoid(
                "removeCookie",
                name);
        }

        public async ValueTask RemoveCookieAsync(string name)
        {
            _ = name ?? throw new ArgumentNullException(nameof(name));
            
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            await module.InvokeVoidAsync(
                "removeCookie",
                name);
        }
    }
}