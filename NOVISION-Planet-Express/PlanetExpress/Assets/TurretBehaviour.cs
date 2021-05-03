using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Oculus.Platform;
using PlanetExpress.Scripts.Bullets;
using PlanetExpress.Scripts.Core;
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
        
        Debug.Log("Turret started");
        
        EnemyBehaviour[] enemies = FindObjectsOfType<EnemyBehaviour>();

        EnemyBehaviour enemy = enemies.FindClosestFrom(gameObject);

        if (!enemy)
        {
            Debug.LogWarning("Closest enemy is null.");
            return;
        }

        Debug.Log("Closest enemy is " + enemy + ".");


        GameObject bulletPref = Resources.Load<GameObject>("SpaceShip/BulletBig");

        Debug.Log("Bullet is : " + bulletPref.name);

        GameObject bullet = Instantiate(bulletPref, transform);

        Debug.Log("Bullet : " + bullet.name);

        BulletBehaviour bulletBehaviour = bullet.GetComponent<BulletBehaviour>();
        bulletBehaviour.SetProperties(10, 1, Squad.Enemy);
    }
}