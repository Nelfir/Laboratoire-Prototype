namespace PlanetExpress.Scripts.Utils
{
    using UnityEngine;
    using System.Collections;

    /// <summary>
    /// From Unity's wiki :
    /// cameraFacingBillboard.cs ( Neil Carter (NCarter) and Juan Castaneda (juanelo))
    /// </summary>
    public class CameraFacingBillboard : MonoBehaviour
    {
        public Camera m_Camera;

        void Awake()
        {
            m_Camera = Camera.main;
        }

        //Orient the camera after all movement is completed this frame to avoid jittering
        void LateUpdate()
        {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);
        }
    }
}