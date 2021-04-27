using System.Collections.Generic;
using UnityEngine;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopController : MonoBehaviour
    {
        public List<ShopItem> ShopItems = new List<ShopItem>();

        public void Start()
        {
            // SoundManager.Instance.PlaySound("Sons/3. SFX_Tuiles (Son Mécanique)/A) Général/2. Autres/1. Vendre/Vendu-001");
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