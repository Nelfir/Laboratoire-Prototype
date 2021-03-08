using System;
using System.Collections.Generic;
using System.Linq;
using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots.Base;
using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;

namespace PlanetExpress.Scripts
{
    public enum PlacedResult
    {
        OK, // Can be placed
        TypeMismatch, // Object and Slot types do not match
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
        public static PlacedResult CanBePlaced(TileObject tileObject, TileSlot tileSlot, bool printDebug = true)
        {
            // Slot is not empty
            if (!tileSlot.IsEmpty)
            {
                return PlacedResult.NotEmpty;
            }

            // Types do not match
            if (tileObject.TileType != tileSlot.TileType)
            {
                return PlacedResult.TypeMismatch;
            }

            return PlacedResult.OK;
        }

        public void Start()
        {
            var r = GetComponentsInChildren<ArrowController>();

            Debug.Log("[PlanetController] Found " + r.Length + " faces.");

            int SmallCount = 0;
            int BigCount = 0;
            int UnknownCount = 0;

            foreach (ArrowController arrowController in r)
            {
                switch (arrowController.TileType)
                {
                    case TileType.Big:
                        BigCount++;
                        break;
                    case TileType.Small:
                        SmallCount++;
                        break;
                    default:
                        UnknownCount++;
                        Debug.LogError("[PlanetController] Unknown tile type for arrow controller!");
                        break;
                }

                // Register the TileSlot in the PlanetController
                TileSlots.Add(arrowController.TileSlot);
            }


            Debug.Log(
                "[PlanetController] Found " + BigCount + " big faces, " +
                SmallCount + " small faces and " +
                UnknownCount + " unknown faces for a total of " +
                r.Length + " faces.");
        }
    }
}