using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public void Start()
    {
        StartCoroutine(nameof(LetsGo));
    }

    public IEnumerator LetsGo()
    {
        yield return new WaitForSeconds(3);
        
        GazeProvider[] body = FindObjectsOfType<GazeProvider>();

        Debug.Log("Found " + body.Length + " objects of type GazeProvider.");

        this.transform.SetParent(body[0].transform, false);
    }
}