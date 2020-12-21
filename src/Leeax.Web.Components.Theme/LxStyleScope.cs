using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Theme
{
    public class LxStyleScope : ComponentBase, IDisposable
    {
        private StyleBase? _value;
        private StyleContext _context = new StyleContext();

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (RenderElement)
            {
                builder.OpenElement(0, "lx-scope");
                builder.AddAttribute(1, "class", "lx-scope lx-scope-style");
                builder.AddAttribute(2, "data-lx-scope", _value?.Identifier);
            }

            builder.OpenComponent<CascadingValue<StyleContext>>(3);
            builder.AddAttribute(4, nameof(CascadingValue<StyleContext>.Value), _context);
            builder.AddAttribute(5, nameof(CascadingValue<StyleContext>.IsFixed), IsFixed);
            builder.AddAttribute(6, nameof(CascadingValue<StyleContext>.ChildContent), ChildContent);
            builder.CloseComponent();

            if (RenderElement)
            {
                builder.CloseElement();
            }
        }

        public void Dispose()
        {
            if (_value != null)
            {
                StyleHandler.Detach(_value);
            }
        }

        [Inject]
        private IStyleScopeHandler StyleHandler { get; set; }

        /// <summary>
        /// Gets or sets the style for this scope.
        /// </summary>
        [Parameter]
        public StyleBase? Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    if (_value != null)
                    {
                        // Check whether the style has the same identifier
                        // -> If so, ignore the new value
                        if (value != null
                            && _value.Identifier == value.Identifier)
                        {
                            return;
                        }

                        // Detach the old style
                        StyleHandler.Detach(_value);
                    }

                    _value = value;

                    if (_value != null)
                    {
                        // Add the new style
                        StyleHandler.Attach(_value);
                    }

                    _context?.SetStyle(_value);
                }
            }
        }

        /// <summary>
        /// Gets or sets whether the underlying <see cref="CascadingValue{TValue}"/> is fixed.
        /// </summary>
        [Parameter]
        public bool IsFixed { get; set; }

        /// <summary>
        /// Gets or sets whether an &lt;lx-scope&gt; element is rendered around the <see cref="ChildContent"/>.
        /// <para />
        /// If the value is <see langword="false"/>, the scope has to be manually applied using the "data-lx-scope" attribute. If not set the generated CSS variables will not have any effect.
        /// The default value is <see langword="false"/>.
        /// </summary>
        [Parameter]
        public bool RenderElement { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [CascadingParameter]
        public StyleContext? StyleContext
        {
            set => _context.SetAncestor(value);
        }
    }
}