using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;

namespace Leeax.Web.Components.Input
{
	public partial class LxToggle : IEnableable
	{
		public const string ClassName = "lx-toggle";
		public const string VariableBackgroundColor = ClassName + "-background";
		public const string VariableActiveBackgroundColor = ClassName + "-active-background";

		protected override void BuildAttributeSet(AttributeSetBuilder builder)
		{
			base.BuildAttributeSet(builder);

			builder.AddClassAttribute(x => x
				.AddMultiple(ClassName, ClassNames.Unselectable)
				.Add(ClassNames.Border, !Value && Appearance == Appearance.Outlined)
				.Add(ClassNames.Disabled, !IsEnabled)
				.Add(ClassNames.Active, Value)
				.AddAppearance(Appearance)
				.AddSize(Size));

			builder.AddAttribute("role", "switch");
		}

		private void OnValueChanged(ChangeEventArgs args) => ValueChanged.InvokeAsync(args.Value is bool value && value);

		/// <summary>
		/// Gets or sets whether the component should be enabled.
		/// </summary>
		[Parameter]
		public bool IsEnabled { get; set; } = true;

		/// <summary>
		/// Gets or sets the appearance. The default value is <see cref="Appearance.Normal"/>.
		/// <para />
		/// Note: <see cref="Appearance.Inline"/> is not supported.
		/// </summary>
		[Parameter]
		public Appearance Appearance { get; set; } = Appearance.Normal;

        /// <summary>
        /// Gets or sets the size. The default value is <see cref="ComponentSize.Medium"/>.
        /// </summary>
        [Parameter]
		public ComponentSize Size { get; set; } = ComponentSize.Medium;
	}
}