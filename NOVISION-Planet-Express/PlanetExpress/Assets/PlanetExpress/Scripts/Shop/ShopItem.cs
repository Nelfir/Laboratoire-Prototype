using System;
using PlanetExpress.Scripts.Universe.Planet.Shared;
using PlanetExpress.Scripts.Universe.Planet.TileObjects.Base;
using UnityEngine;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopItem : MonoBehaviour
    {
        public string Name;
        public int Cost;
        public TileType TileType;

        public void Start()
        {
            InitInfoCanvas();
        }

        private void InitInfoCanvas()
        {
            // Creates the UI with the name and cost text
            GameObject canvasInfo = Resources.Load<GameObject>("CanvasShopItemDescription");
            GameObject o = Instantiate(canvasInfo, gameObject.transform);

            ShopItemInfo shopItemInfo = o.GetComponent<ShopItemInfo>();
            shopItemInfo.SetInfo(Name, Cost);
        }
    }
}