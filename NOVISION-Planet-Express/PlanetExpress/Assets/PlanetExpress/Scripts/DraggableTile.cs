using System;
using System.Linq;
using PlanetExpress.Scripts.Currency;
using PlanetExpress.Scripts.Planet;
using PlanetExpress.Scripts.Shop;
using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using PlanetExpress.Scripts.Utils.VR;
using UnityEngine;
using Valve.VR.InteractionSystem;

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
            _tileObject = gameObject.AddComponent<TileObject>();
        }

        private void InitHandleHoverEvents()
        {
            // Register the grab events
            InteractableHoverEvents interactableHoverEvents = GetComponent<InteractableHoverEvents>();

            interactableHoverEvents.onAttachedToHand.AddListener(() =>
            {
                Debug.Log("Object " + name + " was attached to hand.");
                _isCurrentlyAttachedToHand = true;
            });

            interactableHoverEvents.onDetachedFromHand.AddListener(() =>
            {
                Debug.Log("Object " + name + " was detached from hand.");
                _isCurrentlyAttachedToHand = false;

                PlacedResult placedResult = PlanetController.Instance.CanBePlaced(_tileObject, nearestTileSlot);

                switch (placedResult)
                {
                    case PlacedResult.OK:
                    {
                        Debug.Log("Placing tile here...");


                        ShopItem shopItem = GetComponent<ShopItem>();

                        if (shopItem)
                        {
                            Debug.Log("Is shop item! Cost : " + shopItem.Cost);

                            if (CurrencyController.Instance.Money < shopItem.Cost)
                            {
                                Debug.LogError("Not enough gold!");
                                return;
                            }

                            CurrencyController.Instance.UpdateMoney(-shopItem.Cost);

                            // Destroy the Shop Item so other instances dont cost as much
                            Destroy(shopItem);
                        }

                        LockToPointOrigin lockToPointOrigin = GetComponent<LockToPointOrigin>();
                        lockToPointOrigin.snapTo = nearestTileSlot.ArrowController.OriginPointCreator.Origin.transform;

                        break;
                    }
                    case PlacedResult.NotEmpty:
                        Debug.LogError("Can't place this ShopItem here. The slot is not empty!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }

        private TileSlot nearestTileSlot;

        public void Update()
        {
            if (_isCurrentlyAttachedToHand)
            {
                var nearestSlot = PlanetController.Instance.GetNearestPlaceableTileSlots(_tileObject, true);

                Debug.Log("There is " + nearestSlot.Count + " possible slot(s).");

                var newNearestTileSlot = nearestSlot.FirstOrDefault();

                if (newNearestTileSlot == null)
                {
                    Debug.Log("Null nearest slot.");
                    return;
                }

                if (nearestTileSlot == null)
                {
                    nearestTileSlot = newNearestTileSlot;
                    nearestTileSlot.SetArrowVisible(true);
                }
                else
                {
                    nearestTileSlot.SetArrowVisible(false);
                    nearestTileSlot = newNearestTileSlot;
                    nearestTileSlot.SetArrowVisible(true);
                }
            }
            else
            {
                if (nearestTileSlot != null)
                {
                    nearestTileSlot.SetArrowVisible(false);
                    nearestTileSlot = null;
                }
            }
        }
    }
}