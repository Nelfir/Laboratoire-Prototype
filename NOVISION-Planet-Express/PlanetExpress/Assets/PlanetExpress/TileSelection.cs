using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using UnityEngine;


public enum TileSelectionStatus
{
    Hidden,
    Okay,
    Error,
    Replacing
}

public class TileSelection : MonoBehaviour
{
    public Material Okay;
    public Material Error;
    public Material Replacing;

    public GameObject Arrow;
    public GameObject Corner;

    private Renderer _arrowRenderer;
    private Renderer _selectionRenderer;

    private TileSelectionStatus _status;

    public void Start()
    {
        _arrowRenderer = Arrow.GetComponent<Renderer>();
        _selectionRenderer = Corner.GetComponent<Renderer>();
    }

    private bool TileInSlot = false;

    public void SetHasTileInSlot(bool tileInSlot)
    {
        TileInSlot = tileInSlot;
        UpdateLook();
    }

    public void SetStatus(TileSelectionStatus status)
    {
        _status = status;
        UpdateLook();
    }

    private void UpdateLook()
    {
        // Shows the underlying tile by default
        GetComponentInParent<TileSlot>().GetComponent<Renderer>().enabled = true;

        if (_status == TileSelectionStatus.Hidden)
        {
            Arrow.SetActive(false);
            Corner.SetActive(false);
        }
        else
        {
            Arrow.SetActive(true);
            Corner.SetActive(true);

            if (_status == TileSelectionStatus.Okay)
            {
                Arrow.SetActive(true);
                _arrowRenderer.material = Okay;
                _selectionRenderer.material = Okay;

                // Hides the underlying tile
                GetComponentInParent<TileSlot>().GetComponent<Renderer>().enabled = false;
            }
            else if (_status == TileSelectionStatus.Error)
            {
                Arrow.SetActive(false);
                _arrowRenderer.material = Error;
                _selectionRenderer.material = Error;
            }
            else if (_status == TileSelectionStatus.Replacing)
            {
                Arrow.SetActive(false);
                _arrowRenderer.material = Replacing;
                _selectionRenderer.material = Replacing;
            }
        }

        if (TileInSlot)
        {
            // Hides the underlying tile (there is a tile in the slot)
            GetComponentInParent<TileSlot>().GetComponent<Renderer>().enabled = false;
        }
    }
}