using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Utils.NormalFinder;
using UnityEngine;

namespace PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots
{
    /// <summary>
    /// A tile slot on the planet.
    /// It can be filled by a TileObject.
    /// </summary>
    public class TileSlot : Tile
    {
        public TileObject ChildObject { get; private set; }
        public ArrowController ArrowController { get; private set; }

        public bool IsEmpty => ChildObject == null;

        public void SetDefaultGrassTileIsVisible(bool isVisible)
        {
            this.GetComponent<Renderer>().enabled = isVisible;
        }

        public void SetArrowController(ArrowController arrowController)
        {
            this.ArrowController = arrowController;
        }

        public void SetTile(TileObject tileObject)
        {
            ChildObject = tileObject;
        }
    }
}