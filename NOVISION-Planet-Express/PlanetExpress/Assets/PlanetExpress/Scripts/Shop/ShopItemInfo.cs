using System;
using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using TMPro;
using UnityEngine;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopItemInfo : MonoBehaviour
    {
        public TextMeshProUGUI TMP_Name;
        public TextMeshProUGUI TMP_Cost;
        public TextMeshProUGUI TMP_TileType;

        public void Start()
        {
        }

        public void SetInfo(string _name, int _cost, TileType _tileType)
        {
            TMP_Name.text = _name;
            TMP_Cost.text = "$" + _cost;
            TMP_TileType.text = "Tile Type : " + _tileType;
        }
    }
}