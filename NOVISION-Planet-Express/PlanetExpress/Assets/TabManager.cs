using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class TabManager : MonoBehaviour
{
    public List<Button> Buttons = new List<Button>();

    public List<TabController> Panels = new List<TabController>();


    // Start is called before the first frame update
    void Start()
    {
        if (Buttons.Count != Panels.Count)
        {
            Debug.LogError("[TabController] There is " + Buttons.Count + " buttons and " + Panels.Count + " panels. \n" +
                           "The number of buttons and panels should be the same.");
        }

        for (int i = 0; i < Buttons.Count; i++)
        {
            Button b = Buttons[i];
            int index = i;
            b.onClick.AddListener(() => { Show(Panels[index]); });
        }
    }

    private void Show(TabController panel)
    {
        foreach (TabController p in Panels)
        {
            // Hide all but the Panel
            panel.SetIsSelected(panel == p);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}