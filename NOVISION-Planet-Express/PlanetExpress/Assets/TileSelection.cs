using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelection : MonoBehaviour
{
    public Material Okay;
    public Material Error;

    public GameObject Arrow;
    public GameObject Corner;

    private Renderer _arrowRenderer;
    private Renderer _selectionRenderer;

    public void Start()
    {
        _arrowRenderer = Arrow.GetComponent<Renderer>();
        _selectionRenderer = Corner.GetComponent<Renderer>();
    }

    public void SetIsVisible(bool isVisible)
    {
        Arrow.SetActive(isVisible);
        Corner.SetActive(isVisible);
    }

    public void SetIsOkay(bool isOkay)
    {
        if (isOkay)
        {
            Arrow.SetActive(true);
            _arrowRenderer.material = Okay;
            _selectionRenderer.material = Okay;
        }
        else
        {
            Arrow.SetActive(false);
            _arrowRenderer.material = Error;
            _selectionRenderer.material = Error;
        }
    }
}