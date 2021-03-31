using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Microsoft.MixedReality.Toolkit.Experimental.UI
{
    /// <summary>
    /// This component links the NonNativeKeyboard to a TMP_InputField
    /// Put it on the TMP_InputField and assign the NonNativeKeyboard.prefab
    /// </summary>
    [RequireComponent(typeof(TMP_InputField))]
    public class KeyboardTest : MonoBehaviour, IPointerDownHandler
    {
        [Experimental] [SerializeField] public NonNativeKeyboard keyboard = null;

        private TMP_InputField _textMeshProUGUI;

        public void Start()
        {
            _textMeshProUGUI = GetComponent<TMP_InputField>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Opened " + _textMeshProUGUI.text);
            
            keyboard.PresentKeyboard(_textMeshProUGUI.text);

            keyboard.OnClosed += DisableKeyboard;
            keyboard.OnTextSubmitted += DisableKeyboard;
            keyboard.OnTextUpdated += UpdateText;
        }

        private void UpdateText(string text)
        {
            GetComponent<TMP_InputField>().text = text;
        }

        private void DisableKeyboard(object sender, EventArgs e)
        {
            Debug.Log("Closed " + _textMeshProUGUI.text);
            
            keyboard.OnTextUpdated -= UpdateText;
            keyboard.OnClosed -= DisableKeyboard;
            keyboard.OnTextSubmitted -= DisableKeyboard;

            keyboard.Close();
        }
    }
}