using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image ImageCurrent;

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
        
        if (_maxHealth == 0)
        {
            Debug.LogError("Max health is 0! You know what this means ;) Can't divide by 0 ya dingus!");
        }
        else
        {
            ImageCurrent.rectTransform.localScale = new Vector3(
                _currentHealth / (float) _maxHealth,
                localScale.y,
                localScale.z);
        }
    }
}