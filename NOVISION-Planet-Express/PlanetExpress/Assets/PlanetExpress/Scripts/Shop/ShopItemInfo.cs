using System;
using TMPro;
using UnityEngine;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopItemInfo : MonoBehaviour
    {
        public TextMeshProUGUI TMP_Name;
        public TextMeshProUGUI TMP_Cost;

        public void Start()
        {
        }

        public void SetInfo(string _name, int _cost)
        {
            TMP_Name.text = _name;
            TMP_Cost.text = "$" + _cost;
        }
    }
}