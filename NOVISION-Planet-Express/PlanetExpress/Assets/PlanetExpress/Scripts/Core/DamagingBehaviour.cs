using System;
using UnityEngine;

namespace PlanetExpress.Scripts.Core
{
    public class DamagingBehaviour : MonoBehaviour
    {
        [HideInInspector] public Squad Squad;

        [HideInInspector] public int Damage;

        public void Start()
        {
            Rigidbody r = GetComponent<Rigidbody>();

            if (r == null)
            {
                r = gameObject.AddComponent<Rigidbody>();
                r.isKinematic = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Bullet " + name + " collided with " + other.gameObject.name + ".");

            DamageableBehaviour damageable = other.gameObject.GetComponent<DamageableBehaviour>();

            if (damageable)
            {
                Debug.Log("Is damageable!");

                if (damageable.Squad == Squad)
                {
                    Debug.Log("Is of other squad! " + damageable + " , " + Squad);
                }
            }
        }
    }
}