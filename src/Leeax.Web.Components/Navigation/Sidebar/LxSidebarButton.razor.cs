using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Drawing;
using System.Globalization;

namespace Leeax.Web.Components.Navigation
{
    // This class contains parts of https://github.com/dotnet/aspnetcore/blob/8a81194f372fa6fe63ded2d932d379955854d080/src/Components/Web/src/Routing/NavLink.cs
    public partial class LxSidebarButton : ISidebarItem, IEnableable, IDisposable
    {
        public const string ClassName = "lx-sidebarbutton";

        private string? _linkAbsolute;

        public event EventHandler<string?>? ActiveChanged;

        protected override void OnInitialized()
        {
            if (Parent == null) throw new ApplicationException($"Required cascading value of type \"{nameof(LxSidebar)}\" wasn't supplied.");

            Parent.AddChild(this);
            NavManager.LocationChanged += OnLocationChanged;
        }

        protected override void OnParametersSet()
        {
            var link = Convert.ToString(Link, CultureInfo.InvariantCulture);

            if (link != null)
            {
                _linkAbsolute = NavManager.ToAbsoluteUri(link).AbsoluteUri;

                if (!IsActive
                    && ShouldMatch(NavManager.Uri))
                {
                    RaiseActiveChanged();
                }
            }
        }

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, "px-4", ClassNames.HoverDefault, ClassNames.ActiveDefault)
                .Add(ClassNames.Unselectable)
                .Add(ClassNames.Disabled, !IsEnabled)
                .Add(ClassNames.Active, IsActive));

            builder.AddAttribute("data-lx-interaction", "3");
        }

        private string? GetKey() => Key ?? Label;

        private string? GetTextClasses()
        {
            return ClassBuilder.Create("d-block my-auto " + ClassNames.FontWeightSemibold)
                .Add("indented", Icon == null)
                .Build();
        }

        private void OnClicked()
        {
            if (IsActive) return;

            RaiseActiveChanged();

            if (_linkAbsolute != null)
            {
                NavManager.NavigateTo(_linkAbsolute);
            }
        }

        private void OnLocationChanged(object? sender, LocationChangedEventArgs args)
        {
            if (!IsActive
                && ShouldMatch(args.Location))
            {
                RaiseActiveChanged();
                StateHasChanged();
            }
        }

        private void RaiseActiveChanged()
        {
            ActiveChanged?.Invoke(this, GetKey());
        }

        private bool ShouldMatch(string currentUriAbsolute)
        {
            if (_linkAbsolute == null)
            {
                return false;
            }

            if (EqualsHrefExactlyOrIfTrailingSlashAdded(currentUriAbsolute))
            {
                return true;
            }

            if (Match == NavLinkMatch.Prefix
                && IsStrictlyPrefixWithSeparator(currentUriAbsolute, _linkAbsolute))
            {
                return true;
            }

            return false;
        }

        private bool EqualsHrefExactlyOrIfTrailingSlashAdded(string currentUriAbsolute)
        {
            if (string.Equals(currentUriAbsolute, _linkAbsolute, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (currentUriAbsolute.Length == _linkAbsolute!.Length - 1)
            {
                // Special case: highlight links to http://host/path/ even if you're
                // at http://host/path (with no trailing slash)
                //
                // This is because the router accepts an absolute URI value of "same
                // as base URI but without trailing slash" as equivalent to "base URI",
                // which in turn is because it's common for servers to return the same page
                // for http://host/vdir as they do for host://host/vdir/ as it's no
                // good to display a blank page in that case.
                if (_linkAbsolute[_linkAbsolute.Length - 1] == '/'
                    && _linkAbsolute.StartsWith(currentUriAbsolute, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsStrictlyPrefixWithSeparator(string value, string prefix)
        {
            var prefixLength = prefix.Length;
            if (value.Length > prefixLength)
            {
                return value.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                    && (
                        // Only match when there's a separator character either at the end of the
                        // prefix or right after it.
                        // Example: "/abc" is treated as a prefix of "/abc/def" but not "/abcdef"
                        // Example: "/abc/" is treated as a prefix of "/abc/def" but not "/abcdef"
                        prefixLength == 0
                        || !char.IsLetterOrDigit(prefix[prefixLength - 1])
                        || !char.IsLetterOrDigit(value[prefixLength]));
            }

            return false;
        }

        public void Dispose()
        {
            Parent?.RemoveChild(this);
            NavManager.LocationChanged -= OnLocationChanged;
        }

        public bool IsActive => Parent?.ActiveKey != null && Parent.ActiveKey == GetKey();

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        #region Parameters

        [CascadingParameter(Name = nameof(Parent))]
        public LxSidebar? Parent { get; set; }

        /// <summary>
        /// Gets or sets whether the component should be enabled.
        /// </summary>
        [Parameter]
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [Parameter]
        public string? Key { get; set; }

        /// <summary>
        /// Gets or sets the icon source.
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// Gets or sets the text to display.
        /// </summary>
        [Parameter]
        public string? Label { get; set; }

        /// <summary>
        /// Gets or sets the text-transformation.
        /// Is equal to the "text-transform" CSS property.
        /// </summary>
        [Parameter]
        public TextTransform TextTransform { get; set; }

        /// <summary>
        /// Gets or sets a value representing the URL matching behavior.
        /// </summary>
        [Parameter]
        public NavLinkMatch Match { get; set; }

        /// <summary>
        /// Gets or sets the link/url to navigate to when clicked.
        /// </summary>
        [Parameter]
        public string? Link { get; set; }
        #endregion
    }
}