using UnityEngine;

namespace PlanetExpress.Scripts.Enemy
{
    public class EnemyList : MonoBehaviour
    {
        public GameObject EnemyNormal;
        public GameObject EnemySpeedster;
        public GameObject EnemyGlacier;


        public GameObject SpawnNormal()
        {
            return Instantiate(EnemyNormal);
        }

        public GameObject SpawnSpeedster()
        {
            return Instantiate(EnemySpeedster);
        }

        public GameObject SpawnGlacier()
        {
            return Instantiate(EnemyGlacier);
        }
    }
}