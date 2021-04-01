using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Automatically adds a NonNativeKeyboardInputField on the InputFields if there is not one already.
    /// Allows to show the NonNativeKeyboard when selecting the input field.
    /// </summary>
    public class NonNativeKeyboardInputFieldAutoPopulate : MonoBehaviour
    {
        public void Start()
        {
            foreach (var inputField in gameObject.GetComponentsInChildren<TMP_InputField>())
            {
                if (!inputField.GetComponent<NonNativeKeyboardInputField>())
                {
                    inputField.gameObject.AddComponent<NonNativeKeyboardInputField>();
                }
            }
        }
    }
}