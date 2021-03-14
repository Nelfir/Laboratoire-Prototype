using System;
using UnityEngine;
using UnityEngine.Events;

namespace PlanetExpress.Scripts.Core
{
    public class DamageableBehaviour : MonoBehaviour
    {
        public Squad Squad { get; private set; }

        public class DamagedEvent : UnityEvent<int>
        {
        }

        public DamagedEvent OnDamaged;

        public void Awake()
        {
            OnDamaged = new DamagedEvent();
        }

        public void Start()
        {
            Rigidbody r = GetComponent<Rigidbody>();

            if (r == null)
            {
                r = gameObject.AddComponent<Rigidbody>();
                r.isKinematic = true;
            }
        }

        public void Init(Squad _squad)
        {
            Squad = _squad;
        }

        public void Damage(int amount)
        {
            OnDamaged.Invoke(amount);
        }
    }
}