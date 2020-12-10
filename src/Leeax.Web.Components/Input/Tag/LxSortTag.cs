using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Leeax.Web.Components.Input
{
    public class LxSortTag : LxTagBase
    {
        public new const string ClassName = "lx-tag";

        public LxSortTag()
            : base(ClassName, false)
        {
            IsStatic = false;
        }

        protected override Task OnParametersSetAsync()
        {
            if (IsRemovable
                && Direction != SortDirection.Ascending
                && Direction != SortDirection.Descending)
            {
                Direction = SortDirection.Ascending;

                return DirectionChanged.InvokeAsync(Direction);
            }

            return Task.CompletedTask;
        }

        protected override bool IsInteractable() => !IsStatic;

        protected override Task OnToggle()
        {
            if (IsStatic)
            {
                return Task.CompletedTask;
            }

            Direction = Direction switch
            {
                SortDirection.Ascending => SortDirection.Descending,
                SortDirection.Descending => IsRemovable ? SortDirection.Ascending : SortDirection.None,
                _ => SortDirection.Ascending
            };

            return DirectionChanged.InvokeAsync(Direction);
        }

        [Parameter]
        public override string? Icon
        {
            get => Direction switch
            {
                SortDirection.Ascending => "static://arrow_upward",
                SortDirection.Descending => "static://arrow_downward",
                _ => null
            };
            set => throw new ApplicationException($"Parameter \"{nameof(Icon)}\" will be determined automatically and so on cannot be set manually.");
        }

        [Parameter]
        public override bool IsActive
        {
            get => Direction == SortDirection.Ascending || Direction == SortDirection.Descending;
            set => throw new ApplicationException($"Parameter \"{nameof(IsActive)}\" will be determined automatically and so on cannot be set manually.");
        }

        /// <summary>
        /// Gets or sets the sorting direction.
        /// </summary>
        [Parameter]
        public SortDirection Direction { get; set; }

        /// <summary>
        /// Gets or sets the callback which gets invoked whenever <see cref="Direction"/> changes.
        /// </summary>
        [Parameter]
        public EventCallback<SortDirection> DirectionChanged { get; set; }
    }
}