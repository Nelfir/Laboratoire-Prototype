using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlanetExpress.Scripts.Utils.VR.Valve.VR.InteractionSystem.Sample;
using UnityEngine;


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


    namespace Valve.VR.InteractionSystem.Sample
    {
        public class LockToPoint : MonoBehaviour
        {
            public Transform snapTo;
            private Rigidbody body;
            public float snapTime = 2;

            private float dropTimer;

            protected void Start()
            {
                body = GetComponent<Rigidbody>();
            }

            private void FixedUpdate()
            {
                dropTimer += Time.deltaTime / (snapTime / 2);

                body.isKinematic = dropTimer > 1;

                if (dropTimer > 1)
                {
                    //transform.parent = snapTo;
                    transform.position = snapTo.position;
                    transform.rotation = snapTo.rotation;
                }
                else
                {
                    float t = Mathf.Pow(35, dropTimer);

                    body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, Time.fixedDeltaTime * 4);
                    if (body.useGravity)
                        body.AddForce(-Physics.gravity);

                    transform.position = Vector3.Lerp(transform.position, snapTo.position, Time.fixedDeltaTime * t * 3);
                    transform.rotation = Quaternion.Slerp(transform.rotation, snapTo.rotation, Time.fixedDeltaTime * t * 2);
                }
            }
        }
    }
}