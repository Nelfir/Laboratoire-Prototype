using UnityEngine;

namespace PlanetExpress.Scripts.Universe.Planet.Tiles.Shared
{
    public abstract class Tile : MonoBehaviour
    {
        public abstract TileType TileType { get;   }
    }
}