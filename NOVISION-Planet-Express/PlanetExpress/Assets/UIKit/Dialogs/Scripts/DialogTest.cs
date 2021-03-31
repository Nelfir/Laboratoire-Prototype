using DefaultNamespace;
using DefaultNamespace.UIKit;
using UnityEngine;

namespace UIKit
{
    public class DialogTest : MonoBehaviour
    {
        public void Start()
        {
            DialogBuilder dialogBuilder = new DialogBuilder();

            dialogBuilder
                .SetTitle("Hey! This is a pretty sweet Dialog system you got going on.")
                .SetDescription("Yeah! I built it from scratch.")
                .AddButton(new DialogButton("No", DialogButtonType.Error))
                .AddButton(new DialogButton("Yes", DialogButtonType.Primary))
                .OnAnyButtonClicked((action) =>
                {
                    // Print to console
                    Debug.Log("Action : " + action);
                });

            DialogManager.Instance.PopMessage(dialogBuilder.Build());
            
            dialogBuilder
                .SetTitle("This is another dialog!")
                .SetDescription("Nice.")
                .AddButton(new DialogButton("Yeah sure", DialogButtonType.Error))
                .AddButton(new DialogButton("No", DialogButtonType.Error))
                .AddButton(new DialogButton("Yes", DialogButtonType.Primary))
                .OnAnyButtonClicked((action) =>
                {
                    // Print to console
                    Debug.Log("Action : " + action);
                });
            
            DialogManager.Instance.PopMessage(dialogBuilder.Build());
        }
    }
}