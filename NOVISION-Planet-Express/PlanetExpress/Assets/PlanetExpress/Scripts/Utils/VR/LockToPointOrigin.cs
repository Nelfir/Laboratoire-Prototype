using UnityEngine;
using Valve.VR.InteractionSystem.Sample;

namespace PlanetExpress.Scripts.Utils.VR
{
    public class LockToPointOrigin : LockToPoint
    {
        public new void Start()
        {
            GameObject snapToOrigin = new GameObject("SnapToOrigin - " + name);
            snapToOrigin.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            snapToOrigin.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);

            snapTo = snapToOrigin.transform;

            base.Start();
        }
    }
}