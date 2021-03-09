using System;
using System.Collections.Generic;
using PlanetExpress.Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlanetExpress.Scripts.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        public Vector3 TargetPosition;


        public float counter = 0;
        public float duration = 20;

        public Vector3 StartPosition;

        public void Start()
        {
            StartPosition = transform.position;
            transform.rotation = Quaternion.LookRotation(TargetPosition - transform.position);
        }

        private void FixedUpdate()
        {
            Debug.Log(Time.fixedDeltaTime);
            Debug.Log(counter);
            counter += Time.fixedDeltaTime;
            
            transform.position = Vector3.Lerp(StartPosition, TargetPosition, counter / duration);
        }
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
    public float Speed;


    /// <summary>
    /// The level (damage multiplier of the enemy)
    /// </summary>
    public int Level;


    /// <summary>
    /// Bullet Data
    /// </summary>
    public List<GameObject> BulletSpawnPoints;

    public GameObject BulletPrefab;

    public float BulletDamage;
    public float BulletFrequency;

    #endregion

    #region Stats

    public int CurrentHealth;

    public Transform TargetPosition;

    #endregion

    public void Start()
    {
        CurrentHealth = MaxHealth;
        UpdateEnemyUI();
        StartMove(new Vector3(0, 0, 0));
    }

    private void StartMove(Vector3 targetPos)
    {
        EnemyMover enemyMover = gameObject.AddComponent<EnemyMover>();
        enemyMover.TargetPosition = targetPos;
    }

    private void UpdateEnemyUI()
    {
        EnemyUIController UI = GetComponentInChildren<EnemyUIController>();
        UI.TextLevel.text = "Level " + Level;
        UI.TextName.text = Name;
        UI.UpdateHealth(CurrentHealth, MaxHealth);
    }

    public void Update()
    {
        if (TargetPosition)
        {
        }
    }
}