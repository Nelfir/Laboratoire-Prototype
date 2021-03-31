using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TabController : MonoBehaviour
    {
        public string Name;

        public TextMeshProUGUI Text;
        public GameObject TabFocus;

        public Button Button;

        private readonly Color UnselectedColor = new Color(0.29f, 0.67f, 0.97f);
        private readonly Color SelectedColor = new Color(1f, 1f, 1f);

        public void SetIsSelected(bool isSelected)
        {
            if (isSelected)
            {
                Text.color = SelectedColor;
                TabFocus.SetActive(true);
            }
            else
            {
                Text.color = UnselectedColor;
                TabFocus.SetActive(false);
            }
        }
    }
}