using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;

namespace PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base
{
    /// <summary>
    /// A tile Object is an object that can be put in a TileSlot, such as a Turret or a Forest.
    /// </summary>
    public class TileObject : Tile
    {
        public TileSlot ParentSlot;
    }
}