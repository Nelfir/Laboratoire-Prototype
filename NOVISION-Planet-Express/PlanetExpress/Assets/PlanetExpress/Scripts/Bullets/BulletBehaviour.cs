using PlanetExpress.Scripts.Core;
using UnityEngine;

namespace PlanetExpress.Scripts.Bullets
{
    public class BulletBehaviour : MonoBehaviour
    {
        private float _speed = 1;

        private DamagingBehaviour _damagingBehaviour;

        public void Awake()
        {
            _damagingBehaviour = gameObject.AddComponent<DamagingBehaviour>();
        }

        public void SetProperties(int damage, float speed, Squad squad)
        {
            _speed = speed;
            _damagingBehaviour.Squad = squad;
            _damagingBehaviour.Damage = damage;
        }

        private void FixedUpdate()
        {
            transform.position += transform.forward * Time.fixedDeltaTime * _speed;
        }
    }
}