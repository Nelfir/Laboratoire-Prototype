using TMPro;
using UnityEngine;

namespace PlanetExpress.Scripts.Enemy
{
    public class EnemyUIController : MonoBehaviour
    {
        public TextMeshProUGUI TextLevel;
        public TextMeshProUGUI TextName;

        public HealthUI HealthSlider;

        public void UpdateHealth(int currentHealth, int maxHealth)
        {
            HealthSlider.UpdateHealth(currentHealth, maxHealth);
        }
    }
}