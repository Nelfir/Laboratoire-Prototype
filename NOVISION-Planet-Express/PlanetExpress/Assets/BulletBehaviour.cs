using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed = 5;

    /// <summary>
    /// Properties
    /// </summary>
    /// 
    private float _bulletDamage;

    public void SetProperties(float damage)
    {
        _bulletDamage = damage;
    }

    private void FixedUpdate()
    {
        transform.position += transform.forward * Time.fixedDeltaTime * Speed;
    }
}