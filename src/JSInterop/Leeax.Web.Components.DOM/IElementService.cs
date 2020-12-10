using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.DOM
{
    // Important: All "InsertMarkup" and "RemoveElement" methods could break the diffing algorithm if used wrong
    public interface IElementService
    {
        bool InsertMarkup(string parentSelector, string? value, InsertionPosition position, MarkupType type);

        bool InsertMarkup(ElementReference parentElement, string? value, InsertionPosition position, MarkupType type);

        ValueTask<bool> InsertMarkupAsync(string parentSelector, string? value, InsertionPosition position, MarkupType type);

        ValueTask<bool> InsertMarkupAsync(ElementReference parentElement, string? value, InsertionPosition position, MarkupType type);

        bool RemoveElement(string selector);

        bool RemoveElement(ElementReference element);

        ValueTask<bool> RemoveElementAsync(string selector);

        ValueTask<bool> RemoveElementAsync(ElementReference element);

        void ScrollIntoView(ElementReference element, ScrollIntoViewAlignment block, ScrollIntoViewAlignment inline, bool smooth = false);

        void ScrollIntoView(ElementReference element, bool smooth = false);

        ValueTask ScrollIntoViewAsync(ElementReference element, ScrollIntoViewAlignment block, ScrollIntoViewAlignment inline, bool smooth = false);

        ValueTask ScrollIntoViewAsync(ElementReference element, bool smooth = false);

        ElementBoundingClientRect? GetBoundingClientRect(ElementReference element);

        ValueTask<ElementBoundingClientRect?> GetBoundingClientRectAsync(ElementReference element);

        ElementPositionInfo? GetPosition(ElementReference element);

        ValueTask<ElementPositionInfo?> GetPositionAsync(ElementReference element);

        long AddClickOutsideOfElementHandler(ElementReference[] elements, Action handler);

        bool RemoveClickOutsideOfElementHandler(long handlerId);

        ValueTask<long> AddClickOutsideOfElementHandlerAsync(ElementReference[] elements, Action handler);

        ValueTask<bool> RemoveClickOutsideOfElementHandlerAsync(long handlerId);
    }
}