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


    public AudioSource AudioSource;
    public AudioClip SamNarrationAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        OnRegisterCompleted.onClick.AddListener(() => Show(LoginUI));

        OnSignupInstead.onClick.AddListener(() => Show(RegisterUI));

        OnLoginInstead.onClick.AddListener(() => Show(LoginUI));
        OnSettingsClose.onClick.AddListener(() =>
            {
                Show(ActualGame);
                AudioSource.PlayOneShot(SamNarrationAudioClip);
            }
        );

        OnForgotPassword.onClick.AddListener(() => Show(ForgotPassword));
        OnForgotPasswordCompleted.onClick.AddListener(() => Show(LoginUI));

        Show(RegisterUI);
    }

    private void Show(Object show)
    {
        RegisterUI.SetActive(show == RegisterUI);
        LoginUI.SetActive(show == LoginUI);
        ForgotPassword.SetActive(show == ForgotPassword);
        ActualGame.SetActive(show == ActualGame);
    }
}