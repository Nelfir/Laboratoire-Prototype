using TMPro;
using UnityEngine;

namespace PlanetExpress.Scripts.Shop
{
    [DefaultExecutionOrder(-999)]
    public class ShopItemInfo : MonoBehaviour
    {
        public TextMeshProUGUI TMP_Name;
        public TextMeshProUGUI TMP_Cost;
        public TextMeshProUGUI TMP_TileType;

        public void SetInfo(string _name, int _cost)
        {
            
            Debug.Log("Setting name of " + _name);
            
            TMP_Name.text = _name;
            TMP_Cost.text = "$" + _cost;
        }
    }
}