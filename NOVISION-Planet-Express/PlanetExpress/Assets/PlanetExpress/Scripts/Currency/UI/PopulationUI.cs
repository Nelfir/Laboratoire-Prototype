using TMPro;
using UnityEngine;

namespace PlanetExpress.Scripts.Currency.UI
{
    public class PopulationUI : MonoBehaviour
    {
        public TextMeshProUGUI CurrencyText;

        // Start is called before the first frame update
        void Start()
        {
            PopulationController.Instance.OnValueChangeEvent.AddListener((val) =>
            {
                // Update the UI text
                CurrencyText.text = val.ToString();
            });
        }
    }
}