using System;
using System.Globalization;
using Leeax.Web.Components.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Leeax.Web.Components.Presentation
{
    public class LxTableViewLinkColumn : TableViewColumnBase
    {
        private Binding _textBinding;
        private Binding _hrefBinding;
        private string? _text;
        private IConverter? _textConverter;
        private string? _href;
        private IConverter? _hrefConverter;
        private bool _sortable = false;
        private string? _format;
        private IFormatProvider? _formatProvider;

        protected override void OnParametersSet()
        {
            Binding.TryParse(Text, out _textBinding);
            Binding.TryParse(Href, out _hrefBinding);

            SortingEnabled = Sortable;
            Comparer = _textBinding.HasValue ? new TableViewColumnComparer(_textBinding) : null;
        }

        public override void BuildRenderTree(RenderTreeBuilder builder, object? value)
        {
            object? result1 = Text;
            if (_textBinding.HasValue)
            {
                ReflectionHelper.TryGetPropertyValue(value, _textBinding, out result1);
            }

            object? result2 = Href;
            if (_hrefBinding.HasValue)
            {
                ReflectionHelper.TryGetPropertyValue(value, _hrefBinding, out result2);
            }

            if (HrefConverter != null)
            {
                result2 = HrefConverter.ConvertTypeSafe<string>(result2);
            }

            if (result1 == null)
            {
                result1 = result2;
            }
            else if (TextConverter != null)
            {
                result1 = TextConverter.ConvertTypeSafe<string>(result1);
            }

            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "href", result2?.ToString());
            builder.AddEventStopPropagationAttribute(2, "onclick", true);
            builder.AddContent(3, Format != null && result1 is IFormattable formattableResult
                ? formattableResult.ToString(Format, FormatProvider ?? CultureInfo.InvariantCulture)
                : result1?.ToString());
            builder.CloseElement();
        }

        /// <summary>
        /// Gets or sets the text to display.
        /// This value supports binding with the curly bracket syntax, e.g. "{Model.Name}".
        /// </summary>
        [Parameter]
        public string? Text
        {
            get => _text;
            set => RaisePropertyChanged(ref _text, value);
        }

        /// <summary>
        /// Gets or sets a converter for the text. Especially useful for binding.
        /// </summary>
        [Parameter]
        public IConverter? TextConverter
        {
            get => _textConverter;
            set => RaisePropertyChanged(ref _textConverter, value);
        }

        /// <summary>
        /// Gets or sets the (underlying) href/link.
        /// This value supports binding with the curly bracket syntax, e.g. "{Model.Url}".
        /// </summary>
        [Parameter]
        public string? Href
        {
            get => _href;
            set => RaisePropertyChanged(ref _href, value);
        }

        /// <summary>
        /// Gets or sets a converter for the href/link. Especially useful for binding.
        /// </summary>
        [Parameter]
        public IConverter? HrefConverter
        {
            get => _hrefConverter;
            set => RaisePropertyChanged(ref _hrefConverter, value);
        }

        /// <summary>
        /// Gets or sets whether the column is sortable.
        /// </summary>
        [Parameter]
        public bool Sortable
        {
            get => _sortable;
            set => RaisePropertyChanged(ref _sortable, value);
        }

        /// <summary>
        /// Gets or sets the format for formatting the <see cref="Text"/>.
        /// </summary>
        [Parameter]
        public string? Format
        {
            get => _format;
            set => RaisePropertyChanged(ref _format, value);
        }

        /// <summary>
        /// Gets or set the <see cref="IFormatProvider"/> for formatting the <see cref="Text"/>.
        /// </summary>
        [Parameter]
        public IFormatProvider? FormatProvider
        {
            get => _formatProvider;
            set => RaisePropertyChanged(ref _formatProvider, value);
        }
    }
}