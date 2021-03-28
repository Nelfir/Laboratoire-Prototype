using UnityEngine;

namespace PlanetExpress.Scripts
{
    public class SimpleAttach : MonoBehaviour
    {
        /*
        private Interactable _interactable;

        private void Start()
        {
            _interactable = GetComponent<Interactable>();
        }

        private void OnHandHoverBegin(Hand hand)
        {
            Debug.Log("Showing hand hover...");
            hand.ShowGrabHint();
        }

        private void OnHandHoverEnd(Hand hand)
        {
            Debug.Log("Hiding hand hover...");
            hand.HideGrabHint();
        }

        private void HandHoverUpdate(Hand hand)
        {
            // The type of input
            GrabTypes grabType = hand.GetGrabStarting();

            bool isGrabEnding = hand.IsGrabEnding(gameObject);

            if (_interactable.attachedToHand == null && grabType != GrabTypes.None)
            {
                Debug.Log("Attached object!");
                hand.AttachObject(gameObject, grabType);
                hand.HoverLock(_interactable);
                hand.HideGrabHint();
            }
            else
            {
                Debug.Log("Detached object!");
                hand.DetachObject(gameObject);
                hand.HoverUnlock(_interactable);
            }
        }*/
    }
}