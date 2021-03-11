using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image ImageCurrent;
    public Image ImageMax;

    private int _currentHealth;
    private int _maxHealth;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        _currentHealth = currentHealth;
        _maxHealth = maxHealth;

        UpdateUI();
    }

    private void UpdateUI()
    {
        var localScale = ImageCurrent.rectTransform.localScale;

        ImageCurrent.rectTransform.localScale = new Vector3(
            _currentHealth / (float) _maxHealth,
            localScale.y,
            localScale.z);
    }
}