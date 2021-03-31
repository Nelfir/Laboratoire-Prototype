using UnityEngine.Events;

namespace DefaultNamespace
{
    /// <summary>
    /// The type of the button.
    /// </summary>
    public enum DialogButtonType
    {
        Primary,
        Info,
        Warning,
        Error
    }
    
    /// <summary>
    /// Defines a button
    /// </summary>
    public class DialogButton
    {
        public string Text;
        public DialogButtonType DialogButtonType;

        public DialogButton(string text, DialogButtonType dialogButtonType)
        {
            Text = text;
            DialogButtonType = dialogButtonType;
        }
    }
}