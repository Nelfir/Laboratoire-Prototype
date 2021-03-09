using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    public TextMeshProUGUI TextLevel;
    public TextMeshProUGUI TextName;

    public Slider HealthSlider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        HealthSlider.value = currentHealth / (float) maxHealth;
    }
}