using System.Collections.Generic;
using System.Linq;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using PlanetExpress.Scripts.Utils;
using PlanetExpress.Scripts.Utils.NormalFinder;
using PlanetExpress.Scripts.Utils.Scripts.Utils.Objects;
using UnityEngine;
using Valve.VR.InteractionSystem.Sample;

namespace PlanetExpress.Scripts.Planet
{
    /// <summary>
    /// The PlanetController handles the init logic of the tiles and placing and removing tiles from its surface.
    /// </summary>
    public class PlanetController : Singleton<PlanetController>
    {
        [HideInInspector] public List<TileSlot> TileSlots;

        /// <summary>
        /// Returns the nearest placeable TileSlot from the given TileObject's position.
        /// </summary>
        public TileSlot GetNearestTileSlot(TileObject tileObject)
        {
            return TileSlots.OrderBy(t => (t.gameObject.transform.position - tileObject.transform.position).sqrMagnitude)
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns the nearest placeable TileSlot from the given TileObject's position.
        /// </summary>
        /* public List<TileSlot> GetNearestPlaceableTileSlots(TileObject tileObject, bool onlyPlaceable)
         {
             return TileSlots
                 .Where(x => IsEmpty(x))
                 .OrderBy(t => (t.gameObject.transform.position - tileObject.transform.position).sqrMagnitude)
                 .ToList();
         }*/

        /// <summary>
        /// Returns true if the slot type match and the slot is empty.
        /// </summary>
        public bool IsEmpty(TileSlot tileSlot, bool printDebug = true)
        {
            // Slot is not empty
            if (!tileSlot.IsEmpty)
            {
                return false;
            }

            return true;
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

        public void Place(TileObject obj, TileSlot slot)
        {
            //

            if (!IsEmpty(slot))
            {
                Debug.Log("Replacing!");

                if (obj.ParentSlot == null)
                {
                    Debug.LogError("Can't replace, as this DraggableTile is coming from nowhere!");
                }
                else
                {
                    Debug.Log("Flipping!");

                    // Flip the script!
                    Set(slot.ChildObject, obj.ParentSlot);
                    Set(obj, slot);
                }
            }
            else
            {
                Debug.Log("Not replacing! Just setting...");
                Set(obj, slot);
            }
        }

        private void Set(TileObject obj, TileSlot slot)
        {
            // Tell the tiles lot it's now empty
            if (obj.ParentSlot != null)
            {
                obj.ParentSlot.SetTile(null);
                obj.ParentSlot.ArrowController.SetHasTileInSlot(false);
                obj.ParentSlot = null;
            }

            LockToPoint lockToPointOrigin = obj.GetComponent<LockToPoint>();
            lockToPointOrigin.snapTo = slot.ArrowController.OriginPointCreator.TileOriginTransform.transform;

            obj.ParentSlot = slot;
            slot.SetTile(obj);

            //
            slot.ArrowController.SetHasTileInSlot(slot.ChildObject != null);
        }

        /// <summary>
        /// Returns the next Tile the enemies will attack.
        /// </summary>
        public Vector3 GetNextEnemyTargetTile()
        {
            List<TileObject> possibleObjects = new List<TileObject>();

            foreach (TileSlot slot in TileSlots)
            {
                if (slot.IsEmpty)
                    continue;

                TileObject obj = slot.ChildObject;
                possibleObjects.Add(obj);
            }

            if (possibleObjects.Count == 0)
            {
                Debug.LogError("There is no tiles to be targeted on this planet!");
                return Vector3.negativeInfinity;
            }

            return possibleObjects.ToArray().GetRandom().transform.position;
        }
    }
}