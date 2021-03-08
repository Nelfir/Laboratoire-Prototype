using System;
using PlanetExpress.Scripts.Universe.Planet.TileObjects.Base;
using UnityEngine;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopItem : MonoBehaviour
    {
        public string Name;
        public int Cost;
        public TileObject TileObject;


        public void Start()
        {
            ShopItemInfo shopItemInfo = gameObject.AddComponent<ShopItemInfo>();
            shopItemInfo.SetInfo(Name, Cost);

        }
    }
}