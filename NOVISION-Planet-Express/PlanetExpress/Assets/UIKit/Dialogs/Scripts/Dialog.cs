using System.Collections.Generic;
using UnityEngine.Events;

namespace DefaultNamespace.UIKit
{
    public class Dialog
    {
        public string Title;
        public string Description;

        public List<DialogButton> Buttons;

        public UnityAction<DialogButton> OnButtonClicked;
    }
}