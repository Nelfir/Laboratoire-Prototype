using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Creates a point at the origin of the mesh.
/// </summary>
public class OriginPointCreator : MonoBehaviour
{
    [HideInInspector] public GameObject Origin;

    private GameObject _prefabSphereRotation;

    // Start is called before the first frame update
    void Awake()
    {
        if (_prefabSphereRotation == null) _prefabSphereRotation = Resources.Load<GameObject>("DirectionSphere");

        Origin = Instantiate(_prefabSphereRotation);
        Origin.transform.localScale = new Vector3(3, 3, 3);
        Origin.transform.position = Vector3.zero;
        Origin.transform.SetParent(transform.parent, false);
    }

    public void SetRotation(Quaternion rotation)
    {
        Origin.transform.rotation = rotation;
        Origin.transform.eulerAngles -= new Vector3(90, 0, 0);
    }
}