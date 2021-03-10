using UnityEngine;

namespace PlanetExpress.Scripts.Core
{
    public class DamageableBehaviour : MonoBehaviour
    {
        public Squad Squad { get; private set; }
        public int Health { get; private set; }

        public void Init(Squad _squad, int _health)
        {
            Squad = _squad;
            Health = _health;
        }

        public void UpdateHealth(int add)
        {
            Health += add;
        }
    }
}