using PlanetExpress.Scripts.Bullets;
using PlanetExpress.Scripts.Enemy;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using System.Collections;
using UnityEngine;

namespace PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Big
{
    public class Turret : TileObject
    {



        public void Start()
        {
            StartCoroutine(nameof(StartEnemyAim));
        }

        private IEnumerator StartEnemyAim()
        {
            yield return new WaitForSeconds(15f);

            FindNewEnemies();

            StartCoroutine(nameof(StartEnemyAim));

        }

        private void FindNewEnemies()
        {
            EnemyBehaviour[] enemies = GameObject.FindObjectsOfType<EnemyBehaviour>();

            float maxDist = float.MaxValue;
            EnemyBehaviour enemyBehaviour = null;

            Debug.Log("Looking for closest enemy...");

            foreach (EnemyBehaviour enemy in enemies)
            {

                float cruDist = (transform.position - enemyBehaviour.transform.position).sqrMagnitude;

                if (enemyBehaviour == null)
                {
                    enemyBehaviour = enemy;
                    maxDist = cruDist;
                    continue;
                }

                if (cruDist < maxDist)
                {
                    enemyBehaviour = enemy;
                    maxDist = cruDist;
                }

            }


            Debug.Log("Closest enemy is at " + maxDist + " and name is " + enemyBehaviour.name + ".");

            GameObject bulletPref = Resources.Load<GameObject>("SpaceShip/BulletBig");


            Debug.Log("Bullet is : " + bulletPref.name);

            GameObject bullet = GameObject.Instantiate(bulletPref, transform);

            Debug.Log("Bullet : " + bullet.name);

            BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
            bulletBehaviour.SetProperties(10, 1, Core.Squad.Enemy);

        }
    }
}