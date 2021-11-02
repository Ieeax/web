using Leeax.Web.Builders;
using Leeax.Web.Components.Input;
using System;
using System.Drawing;

namespace Leeax.Web.Components
{
    public static class ClassBuilderExtensions
    {
        public static IClassBuilder AddElevation(this IClassBuilder builder, int level, bool when = true)
        {
            if (!when
                || level < 1
                || level > 6)
            {
                return builder;
            }

            return builder.Add("lx-elevation-l" + level);
        }

        public static IClassBuilder AddFontColor(this IClassBuilder builder, Color background, bool when = true)
        {
            if (!when
                || background == Color.Transparent)
            {
                return builder;
            }

            return builder.Add(ColorHelper.IsDarkColor(background)
                ? "lx-fg-white"
                : "lx-fg-black");
        }

        public static IClassBuilder AddFontColor(this IClassBuilder builder, Color background, Appearance appearance)
        {
            if (appearance == Appearance.Inline
                || appearance == Appearance.Outlined
                || background == Color.Transparent)
            {
                return builder;
            }

            return builder.Add(ColorHelper.IsDarkColor(background)
                ? "lx-fg-white"
                : "lx-fg-black");
        }

        public static IClassBuilder AddSize(this IClassBuilder builder, ComponentSize size, bool when = true)
        {
            if (!when)
            {
                return builder;
            }

            return builder
                .Add(ClassNames.InputComponent)
                .Add(size switch
                {
                    ComponentSize.Tiny => ClassNames.SizeTiny,
                    ComponentSize.Small => ClassNames.SizeSmall,
                    ComponentSize.Medium => ClassNames.SizeMedium,
                    ComponentSize.Large => ClassNames.SizeLarge,
                    ComponentSize.Big => ClassNames.SizeBig,
                    ComponentSize.Huge => ClassNames.SizeHuge,
                    ComponentSize.Massive => ClassNames.SizeMassive,
                    _ => throw new ArgumentOutOfRangeException(nameof(size))
                });
        }

        public static IClassBuilder AddAppearance(this IClassBuilder builder, Appearance appearance, bool when = true)
        {
            if (!when)
            {
                return builder;
            }

            return builder
                .AddElevation(2, appearance == Appearance.Raised)
                .Add(appearance switch
                {
                    Appearance.Normal => ClassNames.AppearanceNormal,
                    Appearance.Inline => ClassNames.AppearanceInline,
                    Appearance.Outlined => ClassNames.AppearanceOutlined,
                    Appearance.Raised => ClassNames.AppearanceRaised,
                    _ => throw new ArgumentOutOfRangeException(nameof(appearance))
                });
        }

        public static IClassBuilder AddTextTransform(this IClassBuilder builder, TextTransform textTransform, bool when = true)
        {
            if (!when
                || textTransform == TextTransform.None)
            {
                return builder;
            }

            return builder
                .Add(textTransform switch
                {
                    TextTransform.Uppercase => ClassNames.Uppercase,
                    TextTransform.Lowercase => ClassNames.Lowercase,
                    _ => throw new ArgumentOutOfRangeException(nameof(textTransform))
                });
        }

        public static IClassBuilder AddTextWrap(this IClassBuilder builder, TextWrap textWrap, bool when = true)
        {
            if (!when)
            {
                return builder;
            }

            return builder
                .Add(textWrap switch
                {
                    TextWrap.NoWrap => ClassNames.TextNoWrap,
                    TextWrap.Anywhere => ClassNames.TextWrapAnywhere,
                    TextWrap.Word => ClassNames.TextWrapWord,
                    _ => throw new ArgumentOutOfRangeException(nameof(textWrap))
                });
        }

        public static IClassBuilder AddAlignment(this IClassBuilder builder, VerticalAlignment alignment, bool when = true)
        {
            if (!when)
            {
                return builder;
            }

            return builder
                .Add(alignment switch
                {
                    VerticalAlignment.Top => ClassNames.VerticalAlignmentTop,
                    VerticalAlignment.Center => ClassNames.VerticalAlignmentCenter,
                    VerticalAlignment.Bottom => ClassNames.VerticalAlignmentBottom,
                    _ => throw new ArgumentOutOfRangeException(nameof(alignment))
                });
        }

        public static IClassBuilder AddAlignment(this IClassBuilder builder, HorizontalAlignment alignment, bool when = true)
        {
            if (!when)
            {
                return builder;
            }

            return builder
                .Add(alignment switch
                {
                    HorizontalAlignment.Left => ClassNames.HorizontalAlignmentLeft,
                    HorizontalAlignment.Center => ClassNames.HorizontalAlignmentCenter,
                    HorizontalAlignment.Right => ClassNames.HorizontalAlignmentRight,
                    _ => throw new ArgumentOutOfRangeException(nameof(alignment))
                });
        }

        public static IClassBuilder AddAlignment(this IClassBuilder builder, Alignment alignment, bool when = true)
        {
            if (!when)
            {
                return builder;
            }

            return builder
                .Add(alignment switch
                {
                    Alignment.Left => ClassNames.AlignmentLeft,
                    Alignment.Top => ClassNames.AlignmentTop,
                    Alignment.Right => ClassNames.AlignmentRight,
                    Alignment.Bottom => ClassNames.AlignmentBottom,
                    Alignment.Center => ClassNames.AlignmentCenter,
                    _ => throw new ArgumentOutOfRangeException(nameof(alignment))
                });
        }
    }
}