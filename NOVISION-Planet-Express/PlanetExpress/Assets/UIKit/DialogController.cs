using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UIKit;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Description;

    public GameObject AlertDialogButtonPanel;

    public UnityAction<DialogButton> OnClicked { get; set; }

    public void Init(Dialog dialog)
    {
        // Set the title and description
        Title.text = dialog.Title;
        Description.text = dialog.Description;

        foreach (DialogButton dialogButton in dialog.Buttons)
        {
            // Spawn the Buttons

            GameObject button = Instantiate(DialogManager.Instance.DialogButtonPrefab, AlertDialogButtonPanel.transform);
            DialogButtonController dialogButtonController = button.GetComponent<DialogButtonController>();
            dialogButtonController.Init(dialogButton);
            dialogButtonController.Button.onClick.AddListener(() => { OnClicked(dialogButton); });
        }
    }
}