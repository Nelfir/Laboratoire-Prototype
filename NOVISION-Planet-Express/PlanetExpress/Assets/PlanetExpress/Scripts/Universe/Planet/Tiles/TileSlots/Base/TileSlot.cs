using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using UnityEngine;

namespace PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots.Base
{
    /// <summary>
    /// A tile slot on the planet.
    /// It can be filled by a TileObject.
    /// </summary>
    public abstract class TileSlot : Tile
    {
        public TileObject ObjectInSlot;
        public ArrowController ArrowController;

        public bool IsEmpty => ObjectInSlot == null;

        public void SetArrowVisible(bool isVisible)
        {
            ArrowController.SetOriginPointIsVisible(isVisible);
        }

        public void SetArrowController(ArrowController arrowController)
        {
            this.ArrowController = arrowController;
        }
    }
}