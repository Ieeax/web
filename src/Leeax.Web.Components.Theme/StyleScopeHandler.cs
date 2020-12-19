using Leeax.Web.Builders;
using Leeax.Web.Components.DOM;
using Leeax.Web.Internal;
using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Theme
{
    public class StyleScopeHandler : IStyleScopeHandler
    {
        private readonly IHeadManager _headManager;
        private readonly Dictionary<string, StyleMetadata> _renderedStyles;
        private readonly Dictionary<Type, ICssValueFormatter> _formatters;

        public StyleScopeHandler(IHeadManager headManager)
            : this(headManager, null)
        {
        }

        public StyleScopeHandler(IHeadManager headManager, Dictionary<Type, ICssValueFormatter>? formatters)
        {
            _headManager = headManager;
            _formatters = formatters ?? new Dictionary<Type, ICssValueFormatter>();
            _renderedStyles = new Dictionary<string, StyleMetadata>();
        }

        public void AddFormatter(Type type, ICssValueFormatter formatter)
        {
            _formatters.Add(type, formatter);
        }

        public void AddFormatter<TValue>(ICssValueFormatter<TValue> formatter)
        {
            _formatters.Add(typeof(TValue), formatter);
        }

        public void Attach(StyleBase value)
        {
            value.ThrowIfNull();

            if (!_renderedStyles.TryGetValue(value.Identifier, out var metadata))
            {
                var stylesheet = CreateStyleFromColorPalette(value);

                _renderedStyles.Add(
                    value.Identifier, 
                    new StyleMetadata(value, stylesheet));

                _headManager.ApplyStyle(
                    stylesheet,
                    "text/css",
                    value.Identifier);
            }
            else
            {
                metadata.CountRendered += 1;
            }
        }

        public void Detach(StyleBase value)
        {
            value.ThrowIfNull();

            if (_renderedStyles.TryGetValue(value.Identifier, out var metadata))
            {
                metadata.CountRendered -= 1;
                if (metadata.CountRendered <= 0)
                {
                    _renderedStyles.Remove(value.Identifier);
                }
            }
        }

        private string CreateStyleFromColorPalette(StyleBase value)
        {
            value.ThrowIfNull();

            var styleBuilder = new StylesheetBuilder();
            var variableBuilder = CssBuilder.Create();

            foreach (var curItem in value.ToDictionary())
            {
                Type valueType;
                string? formattedValue;

                if (curItem.Value == null)
                {
                    formattedValue = null;
                }
                else if (_formatters.TryGetValue(valueType = curItem.Value.GetType(), out var formatter))
                {
                    formattedValue = formatter.Format(valueType, curItem.Value);
                }
                else
                {
                    formattedValue = curItem.Value.ToString();
                }

                var variableName = "--" + curItem.Key;
                variableBuilder.AddProperty(variableName, formattedValue);
            }

            styleBuilder.AddDefinition(
                $"[data-lx-scope=\"{value.Identifier}\"]", 
                variableBuilder.Build());

            return styleBuilder.Build()!;
        }
    }
}