using System.Collections;
using System.Collections.Generic;
using PlanetExpress.Scripts.Utils;
using TMPro;
using UnityEngine;

public class CanvasTile : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Level;

    private HealthUI _healthUI;

    // Start is called before the first frame update
    void Start()
    {
        _healthUI = GetComponentInChildren<HealthUI>();
        gameObject.AddComponent<CameraFacingBillboard>();
    }

    public void UpdateName(string _name)
    {
        Name.text = _name;
    }

    public void UpdateLevel(int level)
    {
        Level.text = "Level " + level;
    }


    public void UpdateHealth(int current, int max)
    {
        _healthUI.UpdateHealth(current, max);
    }
}