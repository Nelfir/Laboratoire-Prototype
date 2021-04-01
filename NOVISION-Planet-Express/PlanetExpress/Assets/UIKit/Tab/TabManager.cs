using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TabManager : MonoBehaviour
{
    [System.Serializable]
    public class SpellAnimationEntry
    {
        public TabController tab;
        public GameObject panel;
    }

    public SpellAnimationEntry[] TabsAndPanels;


    // Start is called before the first frame update
    void Start()
    {
        foreach (SpellAnimationEntry spellAnimationEntry in TabsAndPanels)
        {
            spellAnimationEntry.tab.button.onClick.AddListener(() => { Show(spellAnimationEntry); });
        }
    }

    private void Show(SpellAnimationEntry tab)
    {
        foreach (var tabs in TabsAndPanels)
        {
            // Hide all but the Panel
            tabs.tab.SetIsSelected(tab == tabs);
            tabs.panel.SetActive(tab == tabs);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}