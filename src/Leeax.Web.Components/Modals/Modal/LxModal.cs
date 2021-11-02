﻿using System;
using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Leeax.Web.Components.Modals
{
    public class LxModal : LxComponentBase
    {
        public const string ClassName = "lx-modal";
        
        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(x => x
                .AddMultiple(ClassName, "overflow-hidden", "flex-col", "lx-elevation-l5")
                .AddAlignment(Alignment));
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);
            
            builder.OpenElement(0, "div");
            builder.AddMultipleAttributes(1, AttributeSet);
            builder.AddContent(2, ChildContent);
            builder.CloseElement();
        }

        /// <summary>
        /// Gets or sets the alignment of the modal.
        /// The default value is <see cref="Alignment.Center"/>.
        /// </summary>
        [Parameter]
        public Alignment Alignment { get; set; } = Alignment.Center;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}