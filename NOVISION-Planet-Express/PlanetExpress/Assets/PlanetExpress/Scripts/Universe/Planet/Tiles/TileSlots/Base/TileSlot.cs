using PlanetExpress.Scripts.Universe.Planet.Shared;
using PlanetExpress.Scripts.Universe.Planet.TileObjects.Base;

namespace PlanetExpress.Scripts.Universe.Planet.TileSlots.Base
{
    /// <summary>
    /// A tile slot on the planet.
    /// It can be filled by a TileObject.
    /// </summary>
    public abstract class TileSlot : Tile
    {
        public TileObject ObjectInSlot;

        public bool IsEmpty => ObjectInSlot == null;
        
    }
}