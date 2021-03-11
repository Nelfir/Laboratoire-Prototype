﻿using System;
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
using Valve.VR.InteractionSystem.Sample;

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
            });
        }

        private void HandleShopItem(ShopItem shopItem)
        {
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