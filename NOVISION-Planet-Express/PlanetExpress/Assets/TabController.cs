using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TabController : MonoBehaviour
    {
        public string Name;

        public GameObject TabFocus;

        private readonly Color UnselectedColor = new Color(0.29f, 0.67f, 0.97f);
        private readonly Color SelectedColor = new Color(1f, 1f, 1f);

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] internal Button button;

        public void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            button = GetComponent<Button>();
        }

        public void SetIsSelected(bool isSelected)
        {
            if (isSelected)
            {
                _text.color = SelectedColor;
                TabFocus.SetActive(true);
            }
            else
            {
                _text.color = UnselectedColor;
                TabFocus.SetActive(false);
            }
        }
    }
}