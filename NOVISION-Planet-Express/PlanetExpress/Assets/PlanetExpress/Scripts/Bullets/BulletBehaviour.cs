using PlanetExpress.Scripts.Core;
using UnityEngine;

namespace PlanetExpress.Scripts.Bullets
{
    public class BulletBehaviour : MonoBehaviour
    {
        private Squad _squad;
        private float _speed = 5;
        private float _bulletDamage;

        public void SetProperties(float _damage, float _speed, Squad _squad)
        {
            _bulletDamage = _damage;
            this._speed = _speed;
            this._squad = _squad;
        }

        private void FixedUpdate()
        {
            transform.position += transform.forward * Time.fixedDeltaTime * _speed;
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Bullet " + name + " collided with " + other.gameObject.name + ".");
        
            DamageableBehaviour damageable = other.gameObject.GetComponent<DamageableBehaviour>();

            if (damageable)
            {
                if (damageable.Squad == _squad)
                {
                
                }
            }
        }
    }
}