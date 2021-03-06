﻿using UnityEngine;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopItem : MonoBehaviour
    {
        public string Name;
        public int Cost;

        public GameObject canvasInfo;

        public void Start()
        {
            InitInfoCanvas();
        }
        
        private void InitInfoCanvas()
        {
            // Creates the UI with the name and cost text
            GameObject o = Resources.Load<GameObject>("CanvasShopItemDescription");
            canvasInfo = Instantiate(o, gameObject.transform);

            ShopItemInfo shopItemInfo = canvasInfo.GetComponent<ShopItemInfo>();
            shopItemInfo.SetInfo(Name, Cost);
        }

        public void OnDestroy()
        {
            Destroy(canvasInfo);
        }
    }
}