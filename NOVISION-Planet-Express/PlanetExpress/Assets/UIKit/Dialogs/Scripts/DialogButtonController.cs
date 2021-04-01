using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DefaultNamespace
{
    /// <summary>
    /// Controls the Button on an Dialog.
    /// </summary>
    public class DialogButtonController : MonoBehaviour
    {
        public Sprite Blue,
            Dark,
            Gray,
            Green,
            Orange,
            Purple,
            Red,
            Sky,
            White,
            Yellow;

        public Image Background;
        public TextMeshProUGUI Text;
        public Button Button;

        private DialogButton _dialogButton;

        public void Init(DialogButton dialogButton)
        {
            _dialogButton = dialogButton;

            // Set the text and the button callback to DialogManager
            Text.text = dialogButton.Text;

            Debug.Log("Button type : " + dialogButton.DialogButtonType);
            
            switch (dialogButton.DialogButtonType)
            {
                case DialogButtonType.Primary:
                    SetBackground(Yellow);
                    break;
                case DialogButtonType.Info:
                    SetBackground(Yellow);
                    break;
                case DialogButtonType.Warning:
                    SetBackground(Orange);
                    break;
                case DialogButtonType.Error:
                    SetBackground(Red);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetBackground(Sprite sprite)
        {
            Background.sprite = sprite;
        }
    }
}