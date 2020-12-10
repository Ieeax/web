using Leeax.Web.Builders;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Leeax.Web.Components.Modals
{
    public partial class LxMessageBox : IModelComponent<MessageBoxModel>
    {
        public const string ClassName = "lx-messagebox";

        protected override void BuildAttributeSet(AttributeSetBuilder builder)
        {
            builder.AddClassAttribute(ClassName, ClassNames.FlexColumn);
        }

        private void OnButtonClicked(DialogResult result)
        {
            Model!.DialogResult = result;
            Model.RaiseClosedEvent();
        }

        private IEnumerable<DialogResult> GetResults()
        {
            return Model!.Buttons switch
            {
                MessageBoxButtons.OK => new[] { DialogResult.OK },
                MessageBoxButtons.OKCancel => new[] { DialogResult.OK, DialogResult.Cancel },
                MessageBoxButtons.AbortRetryIgnore => new[] { DialogResult.Abort, DialogResult.Retry, DialogResult.Ignore },
                MessageBoxButtons.YesNoCancel => new[] { DialogResult.Yes, DialogResult.No, DialogResult.Cancel },
                MessageBoxButtons.YesNo => new[] { DialogResult.Yes, DialogResult.No },
                MessageBoxButtons.RetryCancel => new[] { DialogResult.Retry, DialogResult.Cancel },
                _ => throw new NotImplementedException()
            };
        }

        [Parameter]
        public MessageBoxModel? Model { get; set; }
    }
}