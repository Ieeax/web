using System;

namespace Leeax.Web.Components.Modals
{
    public class MessageBoxModel
    {
        private DialogResult _dialogResult;

        public string? Icon { get; set; }

        public string? Title { get; set; }

        public string? Text { get; set; }

        public MessageBoxButtons Buttons { get; set; }

        public DialogResult DialogResult
        { 
            get
            {
                // If no result was set, return a default
                if (_dialogResult == 0)
                {
                    return Buttons switch
                    {
                        MessageBoxButtons.OK => DialogResult.OK,
                        MessageBoxButtons.OKCancel => DialogResult.Cancel,
                        MessageBoxButtons.AbortRetryIgnore => DialogResult.Ignore,
                        MessageBoxButtons.YesNoCancel => DialogResult.Cancel,
                        MessageBoxButtons.YesNo => DialogResult.No,
                        MessageBoxButtons.RetryCancel => DialogResult.Cancel,
                        _ => throw new NotImplementedException()
                    };
                }

                return _dialogResult;
            }
            set => _dialogResult = value;
        }
    }
}