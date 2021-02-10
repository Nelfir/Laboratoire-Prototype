using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button btnStart;

    private void Start()
    {
        btnStart.onClick.AddListener(btnStart_Clicked);
    }

//Charger une scene differente
    void btnStart_Clicked()
    {
        SceneManager.LoadScene("New Scene");
    }
}