using System;
using System.Collections.Generic;
using PlanetExpress.Scripts.Universe.Planet.Shared;
using PlanetExpress.Scripts.Universe.Planet.TileObjects.Base;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopController : MonoBehaviour
    {
        public List<ShopItem> ShopItems = new List<ShopItem>();

        public void Start()
        {
            LoadShopItemsAndCreateTileObjects();
        }

        private void LoadShopItemsAndCreateTileObjects()
        {
            foreach (var shopItem in GetComponentsInChildren<ShopItem>())
            {
                switch (shopItem.TileType)
                {
                    case TileType.Small:
                        shopItem.gameObject.AddComponent<SmallTileObject>();
                        break;
                    case TileType.Big:
                        shopItem.gameObject.AddComponent<BigTileObject>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                ShopItems.Add(shopItem);
                
                // Register the grab events
                InteractableHoverEvents interactableHoverEvents = shopItem.GetComponent<InteractableHoverEvents>();
                
                interactableHoverEvents.onAttachedToHand.AddListener(() =>
                {
                    Debug.Log("Object " + shopItem.Name + " was attached to hand.");
                });
                
                interactableHoverEvents.onDetachedFromHand.AddListener(() =>
                {
                    Debug.Log("Object " + shopItem.Name + " was detached to hand.");
                });
                

            }

            Debug.Log("[ShopController] Found " + ShopItems.Count + " shop items.");
        }
    }
}