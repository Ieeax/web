using System;
using System.Globalization;
using Leeax.Web.Components.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Presentation
{
    public class LxTableViewColumn : TableViewColumnBase
    {
        private Binding _textBinding;
        private string? _text;
        private IConverter? _textConverter;
        private bool _sortable = true;
        private string? _format;
        private IFormatProvider? _formatProvider;
        private RenderFragment<object?>? _template;

        protected override void OnParametersSet()
        {
            Binding.TryParse(Text, out _textBinding);

            SortingEnabled = Template == null && _textBinding.HasValue && Sortable;
            Comparer = _textBinding.HasValue ? new TableViewColumnComparer(_textBinding, TextConverter) : null;
        }

        public override void BuildRenderTree(RenderTreeBuilder builder, object? value)
        {
            if (Template != null)
            {
                builder.AddContent(0, Template, value);
                return;
            }

            object? result = Text;
            if (_textBinding.HasValue)
            {
                ReflectionHelper.TryGetPropertyValue(value, _textBinding, out result);
            }

            if (TextConverter != null)
            {
                result = TextConverter.ConvertTypeSafe<string>(result);
            }

            builder.AddContent(0,
                Format != null && result is IFormattable formattableResult
                    ? formattableResult.ToString(Format, FormatProvider ?? CultureInfo.InvariantCulture)
                    : result?.ToString());
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

        [Parameter]
        public RenderFragment<object?>? Template
        { 
            get => _template;
            set => RaisePropertyChanged(ref _template, value);
        }
    }
}