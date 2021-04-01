using System.Collections.Generic;
using UnityEngine.Events;

namespace DefaultNamespace.UIKit
{
    /// <summary>
    /// Allows to build Dialogs using the builder pattern.
    /// </summary>
    public class DialogBuilder
    {
        private Dialog _dialog;

        public DialogBuilder()
        {
            Reset();
        }

        private void Reset()
        {
            _dialog = new Dialog
            {
                Title = "No Title",
                Description = "No Description",
                Buttons = new List<DialogButton>()
            };
        }

        public DialogBuilder SetTitle(string title)
        {
            _dialog.Title = title;
            return this;
        }

        public DialogBuilder SetDescription(string title)
        {
            _dialog.Description = title;
            return this;
        }

        public DialogBuilder AddButton(DialogButton button)
        {
            _dialog.Buttons.Add(button);
            return this;
        }

        public DialogBuilder AddButtons(IEnumerable<DialogButton> buttons)
        {
            _dialog.Buttons.AddRange(buttons);
            return this;
        }

        public DialogBuilder OnAnyButtonClicked(UnityAction<DialogButton> OnButtonPressed)
        {
            _dialog.OnButtonClicked += OnButtonPressed;
            return this;
        }

        public Dialog Build()
        {
            Dialog d = _dialog;
            Reset();

            return d;
        }
    }
}