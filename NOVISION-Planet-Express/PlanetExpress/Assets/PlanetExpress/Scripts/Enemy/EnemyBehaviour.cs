using System.Collections;
using System.Collections.Generic;
using PlanetExpress.Scripts.Bullets;
using PlanetExpress.Scripts.Core;
using UnityEngine;

namespace PlanetExpress.Scripts.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        public Vector3 TargetPosition;
        private Vector3 _startPosition;

        public float counter = 0;
        public float duration = 20;

        public void Start()
        {
            _startPosition = transform.position;
            transform.rotation = Quaternion.LookRotation(TargetPosition - transform.position);
        }

        private void FixedUpdate()
        {
            counter += Time.fixedDeltaTime;
            transform.position = Vector3.Lerp(_startPosition, TargetPosition, counter / duration);
        }
    }

    public class EnemyBehaviour : MonoBehaviour
    {
        #region StaticData

        /// <summary>
        /// Generic Data
        /// </summary>
        public string Name;

        /// <summary>
        /// Health Data
        /// </summary>
        public int MaxHealth;

        /// <summary>
        /// Speed
        /// </summary>
        public float Speed = 1;

        /// <summary>
        /// The level (damage multiplier of the enemy)
        /// </summary>
        public int Level = 1;

        /// <summary>
        /// Bullet Data
        /// </summary>
        public List<GameObject> BulletSpawnPoints;

        public GameObject BulletPrefab;

        public int BulletDamage = 10;


        public float BulletSpeed = 1;

        /// <summary>
        /// Delay between bullets
        /// </summary>
        public float BulletDelay = 1;

        #endregion

        #region Properties

        [HideInInspector] public int CurrentHealth = 0;
        [HideInInspector] public Vector3 TargetPosition;

        #endregion


        public void Start()
        {
            CurrentHealth = MaxHealth;

            UpdateEnemyUI();
        }


        public void StartMove(Vector3 movingTargetPos)
        {
            TargetPosition = movingTargetPos;

            EnemyMover enemyMover = gameObject.AddComponent<EnemyMover>();
            enemyMover.TargetPosition = movingTargetPos;
        }


        public void StartShooting(Vector3 shootingTargetPos)
        {
            ShootingTargetPos = shootingTargetPos;
            StartCoroutine(nameof(StartShootingLoop));
        }

        private Vector3 ShootingTargetPos;

        private IEnumerator StartShootingLoop()
        {
            yield return new WaitForSeconds(BulletDelay);
            SpawnBullets();
            StartCoroutine(nameof(StartShootingLoop));
        }

        private void SpawnBullets()
        {
            foreach (GameObject bulletSpawnPoint in BulletSpawnPoints)
            {
                Vector3 position = bulletSpawnPoint.transform.position;

                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = position;
                bullet.transform.rotation = Quaternion.LookRotation(ShootingTargetPos - bullet.transform.position);

                bullet.GetComponent<BulletBehaviour>().SetProperties(BulletDamage, BulletSpeed, Squad.Enemy);
            }
        }


        private void UpdateEnemyUI()
        {
            EnemyUIController UI = GetComponentInChildren<EnemyUIController>();
            UI.TextLevel.text = "Level " + Level;
            UI.TextName.text = Name;
            UI.UpdateHealth(CurrentHealth, MaxHealth);
        }
    }
}