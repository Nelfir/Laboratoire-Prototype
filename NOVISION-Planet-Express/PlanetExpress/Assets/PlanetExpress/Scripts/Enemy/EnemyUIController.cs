using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PlanetExpress.Scripts.Enemy
{
    public class EnemyUIController : MonoBehaviour
    {
        public TextMeshProUGUI TextLevel;
        public TextMeshProUGUI TextName;

        public Slider HealthSlider;

        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            HealthSlider.value = currentHealth / (float) maxHealth;
        }
    }
}