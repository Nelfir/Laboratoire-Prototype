using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This component links the NonNativeKeyboard to a TMP_InputField
/// Put it on the TMP_InputField and assign the NonNativeKeyboard.prefab
/// </summary>
[RequireComponent(typeof(TMP_InputField))]
public class NonNativeKeyboardInputField : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] public NonNativeKeyboard.LayoutType LayoutType;

    private static NonNativeKeyboard NonNativeKeyboard;

    private TMP_InputField _textMeshProUGUI;

    public void Start()
    {
        if (NonNativeKeyboard == null)
        {
            NonNativeKeyboard = NonNativeKeyboard.Instance;
        }

        _textMeshProUGUI = GetComponent<TMP_InputField>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Opened " + _textMeshProUGUI.text);

        Debug.Log("TMP : " + _textMeshProUGUI);
        Debug.Log("Keyboard : " + NonNativeKeyboard.name);
        NonNativeKeyboard.PresentKeyboard(_textMeshProUGUI.text, LayoutType);

        NonNativeKeyboard.OnClosed += DisableKeyboard;
        NonNativeKeyboard.OnTextSubmitted += DisableKeyboard;
        NonNativeKeyboard.OnTextUpdated += UpdateText;
    }

    private void UpdateText(string text)
    {
        GetComponent<TMP_InputField>().text = text;
    }

    private void DisableKeyboard(object sender, EventArgs e)
    {
        Debug.Log("Closed " + _textMeshProUGUI.text);

        NonNativeKeyboard.OnTextUpdated -= UpdateText;
        NonNativeKeyboard.OnClosed -= DisableKeyboard;
        NonNativeKeyboard.OnTextSubmitted -= DisableKeyboard;

        NonNativeKeyboard.Close();
    }
}