using System.Collections.Generic;
using UnityEngine;

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
                ShopItems.Add(shopItem);
            }

            Debug.Log("[ShopController] Found " + ShopItems.Count + " shop items.");
        }
    }
}