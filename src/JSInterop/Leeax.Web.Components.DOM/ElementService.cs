using Leeax.Web.Components.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Leeax.Web.Components.DOM
{
    public class ElementService : IElementService
    {
        public const string ModuleKey = "__" + nameof(ElementService);
        public const string ModulePath = "./_content/Leeax.Web.Components.DOM/ElementService.min.js";

        private long _actionOutOfElementCallbackId;
        private readonly Dictionary<long, Action> _actionOutOfElementCallbacks = new Dictionary<long, Action>();

        private readonly IJSInProcessObjectReference? _jsInProcessObjectRef;
        private readonly IJSObjectReferenceStore _jsRefStore;

        public ElementService(IJSObjectReferenceStore jsRefStore)
        {
            _jsRefStore = jsRefStore;
            jsRefStore.TryGet(ModuleKey, out _jsInProcessObjectRef);
        }

        public bool InsertMarkup(string parentSelector, string? value, InsertionPosition position, MarkupType type)
        {
            _ = parentSelector ?? throw new ArgumentNullException(nameof(parentSelector));

            return _jsInProcessObjectRef!.Invoke<bool>(
                "insertMarkup",
                parentSelector,
                value,
                MapInsertionPosition(position),
                MapMarkupType(type));
        }

        public bool InsertMarkup(ElementReference parentElement, string? value, InsertionPosition position, MarkupType type)
        {
            return _jsInProcessObjectRef!.Invoke<bool>(
                "insertMarkup",
                parentElement,
                value,
                MapInsertionPosition(position),
                MapMarkupType(type));
        }

        public async ValueTask<bool> InsertMarkupAsync(string parentSelector, string? value, InsertionPosition position, MarkupType type)
        {
            _ = parentSelector ?? throw new ArgumentNullException(nameof(parentSelector));

            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            return await module.InvokeAsync<bool>(
                "insertMarkup",
                parentSelector,
                value,
                MapInsertionPosition(position),
                MapMarkupType(type));
        }

        public async ValueTask<bool> InsertMarkupAsync(ElementReference parentElement, string? value, InsertionPosition position, MarkupType type)
        {
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            return await module.InvokeAsync<bool>(
                "insertMarkup",
                parentElement,
                value,
                MapInsertionPosition(position),
                MapMarkupType(type));
        }

        public bool RemoveElement(string selector)
        {
            _ = selector ?? throw new ArgumentNullException(nameof(selector));

            return _jsInProcessObjectRef!.Invoke<bool>(
                "removeElement",
                selector);
        }

        public bool RemoveElement(ElementReference element)
        {
            return _jsInProcessObjectRef!.Invoke<bool>(
                "removeElement",
                element);
        }

        public async ValueTask<bool> RemoveElementAsync(string selector)
        {
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            return await module.InvokeAsync<bool>(
                "removeElement",
                selector);
        }

        public async ValueTask<bool> RemoveElementAsync(ElementReference element)
        {
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            return await module.InvokeAsync<bool>(
                "removeElement",
                element);
        }

        public void ScrollIntoView(ElementReference element, ScrollIntoViewAlignment block, ScrollIntoViewAlignment inline, bool smooth = false)
        {
            _jsInProcessObjectRef!.InvokeVoid(
                "scrollIntoView",
                element,
                MapScrollIntoViewAlignment(block),
                MapScrollIntoViewAlignment(inline),
                smooth);
        }

        public void ScrollIntoView(ElementReference element, bool smooth = false)
        {
            ScrollIntoView(
                element,
                ScrollIntoViewAlignment.Start,
                ScrollIntoViewAlignment.Nearest,
                smooth);
        }

        public async ValueTask ScrollIntoViewAsync(ElementReference element, ScrollIntoViewAlignment block, ScrollIntoViewAlignment inline, bool smooth = false)
        {
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            await module.InvokeVoidAsync(
                "scrollIntoView",
                element,
                MapScrollIntoViewAlignment(block),
                MapScrollIntoViewAlignment(inline),
                smooth);
        }

        public ValueTask ScrollIntoViewAsync(ElementReference element, bool smooth = false)
        {
            return ScrollIntoViewAsync(
                element, 
                ScrollIntoViewAlignment.Start,
                ScrollIntoViewAlignment.Nearest,
                smooth);
        }

        public ElementBoundingClientRect? GetBoundingClientRect(ElementReference element)
        {
            return _jsInProcessObjectRef!.Invoke<ElementBoundingClientRect>(
                "getBoundingClientRect",
                element);
        }

        public async ValueTask<ElementBoundingClientRect?> GetBoundingClientRectAsync(ElementReference element)
        {
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            return await module.InvokeAsync<ElementBoundingClientRect>(
                "getBoundingClientRect",
                element);
        }

        public ElementPositionInfo? GetPosition(ElementReference element)
        {
            return _jsInProcessObjectRef!.Invoke<ElementPositionInfo>(
                "getPosition",
                element);
        }

        public async ValueTask<ElementPositionInfo?> GetPositionAsync(ElementReference element)
        {
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            return await module.InvokeAsync<ElementPositionInfo>(
                "getPosition",
                element);
        }

        private static string MapScrollIntoViewAlignment(ScrollIntoViewAlignment value)
        {
            return value switch
            {
                ScrollIntoViewAlignment.Start => "start",
                ScrollIntoViewAlignment.Center => "center",
                ScrollIntoViewAlignment.End => "end",
                _ => "nearest"
            };
        }

        private static string MapInsertionPosition(InsertionPosition value)
        {
            return value switch
            {
                InsertionPosition.BeforeBegin => "beforebegin",
                InsertionPosition.AfterBegin => "afterbegin",
                InsertionPosition.BeforeEnd => "beforeend",
                InsertionPosition.AfterEnd => "afterend",
                _ => throw new ArgumentException($"Unknown {nameof(InsertionPosition)} \"{nameof(InsertionPosition)}.{value}\".")
            };
        }

        private static string MapMarkupType(MarkupType value)
        {
            return value switch
            {
                MarkupType.HTML => "html",
                MarkupType.Text => "text",
                _ => throw new ArgumentException($"Unknown {nameof(MarkupType)} \"{nameof(MarkupType)}.{value}\".")
            };
        }

        public long AddClickOutsideOfElementHandler(ElementReference[] elements, Action handler)
        {
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            var id = _actionOutOfElementCallbackId++;

            _actionOutOfElementCallbacks.Add(id, handler);

            _jsInProcessObjectRef!.InvokeVoid(
                "addClickOutsideOfElementHandler",
                DotNetObjectReference.Create(this),
                elements,
                id);

            return id;
        }

        public bool RemoveClickOutsideOfElementHandler(long handlerId)
        {
            if (!_actionOutOfElementCallbacks.ContainsKey(handlerId))
            {
                return false;
            }

            return _jsInProcessObjectRef!.Invoke<bool>(
                "removeClickOutsideOfElementHandler",
                handlerId);
        }

        public async ValueTask<long> AddClickOutsideOfElementHandlerAsync(ElementReference[] elements, Action handler)
        {
            _ = handler ?? throw new ArgumentNullException(nameof(handler));

            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            var id = _actionOutOfElementCallbackId++;

            _actionOutOfElementCallbacks.Add(id, handler);

            await module.InvokeVoidAsync(
                "addClickOutsideOfElementHandler",
                DotNetObjectReference.Create(this),
                elements,
                id);

            return id;
        }

        public async ValueTask<bool> RemoveClickOutsideOfElementHandlerAsync(long handlerId)
        {
            var module = await _jsRefStore
                .ImportOrGetModuleAsync(ModuleKey, ModulePath);

            if (!_actionOutOfElementCallbacks.ContainsKey(handlerId))
            {
                return false;
            }

            return await module.InvokeAsync<bool>(
                "removeClickOutsideOfElementHandler",
                handlerId);
        }

        [JSInvokable]
        public void HandleClickOutsideOfElementCallback(long handlerId)
        {
            if (_actionOutOfElementCallbacks.TryGetValue(handlerId, out var callback))
            {
                _actionOutOfElementCallbacks.Remove(handlerId);

                callback?.Invoke();
                return;
            }

            Console.WriteLine($"[WARN] Unkown 'ClickOutsideOfElement' handler-id '{handlerId}' were passed from javascript.");
        }
    }
}