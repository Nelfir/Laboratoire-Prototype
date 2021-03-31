using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.UIKit;
using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Allows to pop UI alert dialogs.
/// </summary>
public class DialogManager : Singleton<DialogManager>
{
    public GameObject DialogPrefab;
    public GameObject DialogButtonPrefab;

    private DialogController _currentPopupMessage;

    private readonly Queue<DialogController> PendingDialogs = new Queue<DialogController>();


    public void PopQuestion(string title, string question, UnityAction onYes, UnityAction onNo)
    {
        DialogBuilder dialogBuilder = new DialogBuilder();

        DialogButton yes = new DialogButton("No", DialogButtonType.Info);
        DialogButton no = new DialogButton("Yes", DialogButtonType.Primary);

        PopMessage(new DialogBuilder()
            .SetTitle(title)
            .SetDescription(question)
            .AddButton(yes)
            .AddButton(no)
            .OnAnyButtonClicked((action) =>
            {
                // Print to console
                Debug.Log("Action : " + action);

                if (action == yes)
                {
                    onYes.Invoke();
                }
                else if (action == no)
                {
                    onNo.Invoke();
                }
                else
                {
                    Debug.LogError("Unknown action : " + action);
                }
            }).Build());
    }

    /// <summary>
    /// Shows a Dialog to the user.
    /// Spawns a prefab in the scene.
    /// </summary>
    /// <param name="dialog">The Dialog to display. Use DialogBuilder help building a Dialog.</param>
    public void PopMessage(Dialog dialog)
    {
        GameObject popup = Instantiate(DialogPrefab, gameObject.transform);
        DialogController dialogController = popup.GetComponent<DialogController>();
        dialogController.Init(dialog);
        dialogController.gameObject.SetActive(false);
        dialogController.OnClicked += (dialogButton) =>
        {
            dialog.OnButtonClicked.Invoke(dialogButton);

            // Hide the current dialog
            dialogController.gameObject.SetActive(false);
            _currentPopupMessage = null;

            // If there is a pending dialog in the queue
            if (PendingDialogs.Count != 0)
            {
                // Show the next dialog
                Show(PendingDialogs.Dequeue());
            }
        };

        PopMessage(dialogController);
    }

    /// <summary>
    /// The private method that checks if there is already a popup, otherwise shows the Dialog.
    /// </summary>
    private void PopMessage(DialogController dialogController)
    {
        // There is already a popup visible on the screen
        if (_currentPopupMessage)
        {
            // Add the dialog to the pending dialogs
            PendingDialogs.Enqueue(dialogController);
        }
        else
        {
            // Shows the dialog
            Show(dialogController);
        }
    }

    private void Show(DialogController dialogController)
    {
        _currentPopupMessage = dialogController;
        dialogController.gameObject.SetActive(true);
    }
}