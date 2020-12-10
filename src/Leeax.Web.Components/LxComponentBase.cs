using Leeax.Web.Builders;
using Leeax.Web.Components.Theme;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components
{
    public class LxComponentBase : ComponentBase
    {
        protected virtual void BuildAttributeSet(AttributeSetBuilder builder)
        {
        }

        private void BuildAttributeSetInternal(AttributeSetBuilder builder)
        {
            BuildAttributeSet(builder);

            // Info: If parameter names (for components) gets checked "case sensitive" 
            // we would need to correct that in "SetParametersAsync()" method (to "Class" and "Style")
            // -> Currently not the case, but could possible change during development of Blazor

            object? classMergeFactory(object? existingValue) => existingValue == null 
                ? Class
                : (existingValue + " " + Class).TrimEnd();

            object? styleMergeFactory(object? existingValue)
            {
                if (existingValue == null)
                {
                    return Style;
                }

                var value = existingValue.ToString();

                // Determine if we need to add a semicolon ";" to the previous string
                return value!.EndsWith(";")
                    ? existingValue + Style
                    : existingValue + ";" + Style;
            }

            // Merge values of component with the "AttributSetBuilder"
            // -> All attributes which gets applied to the root-element of the component
            builder.AddAttribute("class", classMergeFactory, true, false);
            builder.AddAttribute("style", styleMergeFactory, true, false);

            if (StyleScope != null)
            {
                builder.AddAttribute("data-lx-scope", StyleScope);
            }
        }
        
        [Parameter]
        public string? Class { get; set; }

        [Parameter]
        public string? Style { get; set; }

        [Parameter]
        public string? StyleScope { get; set; }

        [CascadingParameter]
        public StyleContext StyleContext { get; set; }

        /// <summary>
        /// Set of attributes which can be configured trough overriding <see cref="BuildAttributeSet"/>.
        /// Have to be applied to the root-element of your component using the "@attributes"-tag.
        /// Cannot be applied to another component, has to be an element.
        /// </summary>
        protected AttributeSet AttributeSet => AttributeSet.Create(BuildAttributeSetInternal)!;
    }
}