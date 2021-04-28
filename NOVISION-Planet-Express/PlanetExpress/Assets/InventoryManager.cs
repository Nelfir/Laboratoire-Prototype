using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using PlanetExpress.Scripts.Utils.VR;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Transform Follow;

    public void Start()
    {
        StartCoroutine(nameof(LetsGo));
    }

    public IEnumerator LetsGo()
    {
        yield return new WaitForSeconds(3);

        GazeProvider[] body = FindObjectsOfType<GazeProvider>();

        Debug.Log("Found " + body.Length + " objects of type GazeProvider.");

        Follow = body[0].transform;

        foreach (var lockToPointOrigin in gameObject.GetComponentsInChildren<LockToPointOrigin>())
        {
            lockToPointOrigin.UpdateSnapToOrigin();
        }
        
    }


    public void Update()
    {
        if (Follow)
        {
            this.gameObject.transform.position = Follow.position;
        }
    }
}