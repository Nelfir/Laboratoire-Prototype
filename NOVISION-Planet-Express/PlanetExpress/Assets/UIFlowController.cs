using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFlowController : MonoBehaviour
{
    public GameObject RegisterUI;
    public GameObject LoginUI;
    public GameObject SettingsUI;


    public Button OnRegisterCompleted;
    public Button OnLoginInstead;
    public Button OnLoginCompleted;
    public Button OnSettingsClose;


    // Start is called before the first frame update
    void Start()
    {
        OnRegisterCompleted.onClick.AddListener(() => Show(LoginUI));
        OnLoginInstead.onClick.AddListener(() => Show(LoginUI));
        OnLoginCompleted.onClick.AddListener(() => Show(SettingsUI));
        OnSettingsClose.onClick.AddListener(() => Show(null));

        Show(RegisterUI);
    }

    private void Show(Object show)
    {
        RegisterUI.SetActive(show == RegisterUI);
        LoginUI.SetActive(show == LoginUI);
        SettingsUI.SetActive(show == SettingsUI);
    }
}