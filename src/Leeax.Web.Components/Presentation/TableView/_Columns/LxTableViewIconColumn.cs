using Leeax.Web.Components.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Presentation
{
    public class LxTableViewIconColumn : TableViewColumnBase
    {
        private Binding _sourceBinding;
        private string? _source;
        private IConverter? _sourceConverter;
        private Length _size = new Length(1.4, Unit.EM);

        public LxTableViewIconColumn()
        {
            ClassNameCell = "tv-cell-icon";
            SortingEnabled = false;
        }

        protected override void OnParametersSet()
        {
            Binding.TryParse(Source, out _sourceBinding);
        }

        public override void BuildRenderTree(RenderTreeBuilder builder, object? value)
        {
            object? result = Source;
            if (_sourceBinding.HasValue)
            {
                ReflectionHelper.TryGetPropertyValue(value, _sourceBinding, out result);
            }

            if (SourceConverter != null)
            {
                result = SourceConverter.ConvertTypeSafe<string>(result);
            }

            builder.OpenComponent(0, typeof(LxIcon));
            builder.AddAttribute(1, "Size", Size);
            builder.AddAttribute(2, "Source", result?.ToString());
            builder.CloseComponent();
        }

        /// <summary>
        /// Gets or sets the icon source.
        /// This value supports binding with the curly bracket syntax, e.g. "{Model.Icon}".
        /// </summary>
        [Parameter]
        public string? Source
        {
            get => _source;
            set => RaisePropertyChanged(ref _source, value);
        }

        /// <summary>
        /// Gets or sets a converter for the icon source. Especially useful for binding.
        /// </summary>
        [Parameter]
        public IConverter? SourceConverter
        {
            get => _sourceConverter;
            set => RaisePropertyChanged(ref _sourceConverter, value);
        }

        /// <summary>
        /// Gets or sets the icon size.
        /// </summary>
        [Parameter]
        public Length Size
        {
            get => _size;
            set => RaisePropertyChanged(ref _size, value);
        }
    }
}