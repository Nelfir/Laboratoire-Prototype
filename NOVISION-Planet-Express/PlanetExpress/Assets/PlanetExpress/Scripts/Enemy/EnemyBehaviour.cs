using System.Collections.Generic;
using UnityEngine;

namespace PlanetExpress.Scripts.Enemy
{
    public enum BulletType
    {
        Normal,
        Big
    }

    public class EnemyBehaviour : MonoBehaviour
    {
        public BulletType BulletType;

        public List<GameObject> BulletSpawnPoints;
    }
}