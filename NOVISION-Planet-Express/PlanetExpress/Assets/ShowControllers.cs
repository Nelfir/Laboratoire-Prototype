using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowControllers : MonoBehaviour
{
    public bool DoShowControllers;


    public void Start()
    {
        foreach (Hand hand in Player.instance.hands)
        {
            if (DoShowControllers)
            {
                // Shows the controller
                hand.ShowController();
                // Hands will wrap around the controller
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithController);
            }
            else
            {
                // Hide the controller
                hand.HideController();
                // Hands wont wrap around the controller
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }
    }
}