using System.Collections.Generic;
using System.Linq;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using PlanetExpress.Scripts.Utils.NormalFinder;
using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;

namespace PlanetExpress.Scripts.Planet
{
    public enum PlacedResult
    {
        OK, // Can be placed
        NotEmpty // Slot is not empty
    }

    /// <summary>
    /// The PlanetController handles the init logic of the tiles and placing and removing tiles from its surface.
    /// </summary>
    public class PlanetController : Singleton<PlanetController>
    {
        [HideInInspector] public List<TileSlot> TileSlots;

        /// <summary>
        /// Returns the nearest placeable TileSlot from the given TileObject's position.
        /// </summary>
        public List<TileSlot> GetNearestPlaceableTileSlots(TileObject tileObject, bool onlyPlaceable)
        {
            return TileSlots
                .Where(x => CanBePlaced(tileObject, x) == PlacedResult.OK)
                .OrderBy(t => (t.gameObject.transform.position - tileObject.transform.position).sqrMagnitude)
                .ToList();
        }

        /// <summary>
        /// Returns true if the slot type match and the slot is empty.
        /// </summary>
        public PlacedResult CanBePlaced(TileObject tileObject, TileSlot tileSlot, bool printDebug = true)
        {
            // Slot is not empty
            if (!tileSlot.IsEmpty)
            {
                return PlacedResult.NotEmpty;
            }

            return PlacedResult.OK;
        }

        public void Start()
        {
            var r = GetComponentsInChildren<ArrowController>();

            Debug.Log("[PlanetController] Found " + r.Length + " faces.");

            foreach (ArrowController arrowController in r) 
                TileSlots.Add(arrowController.TileSlot);

            Debug.Log(
                "[PlanetController] Found " + r.Length + " faces.");
        }
    }
}