using PlanetExpress.Scripts.Currency;
using PlanetExpress.Scripts.Planet;
using PlanetExpress.Scripts.Shop;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MeteorGrab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InitHandleHoverEvents();
    }

    private void InitHandleHoverEvents()
    {
        // Register the grab events
        InteractableHoverEvents interactableHoverEvents = GetComponent<InteractableHoverEvents>();

        interactableHoverEvents.onAttachedToHand.AddListener(() =>
        {
            // TODO play sound
            CurrencyController.Instance.Value += 10;
            Destroy(gameObject);
        });
    }
}