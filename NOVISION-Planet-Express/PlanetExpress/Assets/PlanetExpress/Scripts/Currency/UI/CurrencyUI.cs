using TMPro;
using UnityEngine;

namespace PlanetExpress.Scripts.Currency.UI
{
    public class CurrencyUI : MonoBehaviour
    {
        public TextMeshProUGUI CurrencyText;

        // Start is called before the first frame update
        void Start()
        {
            CurrencyController.Instance.OnValueChangeEvent.AddListener((val) =>
            {
                 // SoundManager.Instance.PlaySound("Sons/3. SFX_Tuiles (Son Mécanique)/A) Général/2. Autres/1. Vendre/Vendu-001");
                // Update the UI text
                CurrencyText.text = val.ToString();
            });
        }
    }
}