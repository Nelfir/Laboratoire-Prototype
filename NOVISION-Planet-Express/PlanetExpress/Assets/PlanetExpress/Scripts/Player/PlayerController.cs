using UnityEngine;

namespace PlanetExpress.Scripts.Player
{
    /// <summary>
    /// Allows for 'WASD'-like movement.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        // TODO DELETE
        /*public SteamVR_Action_Vector2 input;

        public void Update()
        {
            // Fix for using both teleportation and moving at the same time
            if (input.axis.magnitude > 0.1f)
            {
                Vector3 direction = Valve.VR.InteractionSystem.Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y)) * Time.deltaTime;
                Vector3 horizontalDirection = Vector3.ProjectOnPlane(direction, Vector3.up);

                transform.position += horizontalDirection;
            }
        }*/
    }
}