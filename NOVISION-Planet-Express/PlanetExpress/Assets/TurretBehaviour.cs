using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using PlanetExpress.Scripts.Enemy;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using UnityEngine;

public class TurretBehaviour : TileObject
{
    public GameObject Gun;

    public TurretSpecs TurretSpecs;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        StartCoroutine(nameof(StartShootingLoop));
    }

    public IEnumerator StartShootingLoop()
    {
        yield return new WaitForSeconds(TurretSpecs.delay);

        Shoot();
        StartCoroutine(nameof(StartShootingLoop));
    }

    private void Shoot()
    {
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();

        EnemyBehaviour enemy = enemies.FindClosestFrom(gameObject);

        if (!enemy)
        {
            Debug.LogWarning("Closest enemy is null.");
            return;
        }

        Debug.Log("Closest enemy is " + enemy + ".");
        
        
        
    }
}