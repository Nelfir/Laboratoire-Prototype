using PlanetExpress.Scripts.Currency;
using PlanetExpress.Scripts.Planet;
using PlanetExpress.Scripts.Shop;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using UnityEngine;

namespace PlanetExpress.Scripts
{
    public class DraggableTile : MonoBehaviour
    {
        private TileObject _tileObject;
        private bool _isCurrentlyAttachedToHand;

        public void Start()
        {
            InitTileObject();
            InitHandleHoverEvents();
        }

        private void InitTileObject()
        {
            _tileObject = gameObject.GetComponent<TileObject>();

            if (_tileObject == null) Debug.LogError("Null tile object!");
        }

        private void InitHandleHoverEvents()
        {
            // Register the grab events
            
            
            // TODO INTERACTION SYSTEM
            
            /*InteractableHoverEvents interactableHoverEvents = GetComponent<InteractableHoverEvents>();

            interactableHoverEvents.onAttachedToHand.AddListener(() =>
            {
                Debug.Log("Object " + name + " was attached to hand.");
                _isCurrentlyAttachedToHand = true;
            });

            interactableHoverEvents.onDetachedFromHand.AddListener(() =>
            {
                Debug.Log("Object " + name + " was detached from hand.");
                _isCurrentlyAttachedToHand = false;

                bool isReplacing = false;

                // Hide the selection
                if (nearestTileSlot != null)
                {
                    nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Hidden);
                }

                Debug.Log("Placing tile here...");

                HandleShopItem(GetComponent<ShopItem>());

                // Calculate the new position
                PlanetController.Instance.Place(_tileObject, nearestTileSlot);
            });*/
        }

        private void HandleShopItem(ShopItem shopItem)
        {
            if (shopItem)
            {
                Debug.Log("Is shop item! Cost : " + shopItem.Cost);

                if (CurrencyController.Instance.Value < shopItem.Cost)
                {
                    Debug.LogError("Not enough gold!");
                    return;
                }

                CurrencyController.Instance.Value -= shopItem.Cost;

                // Destroy the Shop Item so other instances dont cost as much
                Destroy(shopItem);
            }
        }

        private TileSlot nearestTileSlot;

        public void Update()
        {
            if (_isCurrentlyAttachedToHand)
            {
                var nearestSlot = PlanetController.Instance.GetNearestTileSlot(_tileObject);

                if (nearestTileSlot == null)
                {
                    nearestTileSlot = nearestSlot;
                }

                var isEmpty = PlanetController.Instance.IsEmpty(nearestSlot);

                nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Hidden);
                nearestTileSlot = nearestSlot;

                if (isEmpty)
                    nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Okay);
                else
                {
                    if (_tileObject == null)
                    {
                        // Can't replace, this tile is not coming from another tile!
                        // Probably from the Shop.

                        nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Error);
                    }
                    else
                    {
                        // Will replace
                        nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Replacing);
                    }
                }
            }
        }
    }
}