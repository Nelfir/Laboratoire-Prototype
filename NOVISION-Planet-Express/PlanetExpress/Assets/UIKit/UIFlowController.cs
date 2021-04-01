using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIFlowController : MonoBehaviour
{
    [InspectorNote("The UIs that are to be toggled on and off.")]
    public GameObject RegisterUI;

    public GameObject LoginUI;
    public GameObject SettingsUI;
    public GameObject ForgotPassword;
    public GameObject ActualGame;

    [InspectorNote("The buttons that manage the UIs.")]
    public Button OnRegisterCompleted;

    public Button OnSignupInstead;
    public Button OnLoginInstead;
    public Button OnLoginCompleted;
    public Button OnSettingsClose;
    public Button OnForgotPassword;
    public Button OnForgotPasswordCompleted;

    // Start is called before the first frame update
    void Start()
    {
        OnRegisterCompleted.onClick.AddListener(() => Show(LoginUI));

        OnSignupInstead.onClick.AddListener(() => Show(RegisterUI));

        OnLoginInstead.onClick.AddListener(() => Show(LoginUI));
        OnLoginCompleted.onClick.AddListener(() => Show(SettingsUI));
        OnSettingsClose.onClick.AddListener(() => Show(ActualGame));

        OnForgotPassword.onClick.AddListener(() => Show(ForgotPassword));
        OnForgotPasswordCompleted.onClick.AddListener(() => Show(LoginUI));

        Show(RegisterUI);
    }

    private void Show(Object show)
    {
        RegisterUI.SetActive(show == RegisterUI);
        LoginUI.SetActive(show == LoginUI);
        SettingsUI.SetActive(show == SettingsUI);
        ForgotPassword.SetActive(show == ForgotPassword);
        ActualGame.SetActive(show == ActualGame);
    }
}