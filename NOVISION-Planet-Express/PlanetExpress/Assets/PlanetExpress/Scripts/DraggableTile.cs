using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using PlanetExpress.Scripts.Currency;
using PlanetExpress.Scripts.Planet;
using PlanetExpress.Scripts.Shop;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using PlanetExpress.Scripts.Utils.VR;
using PlanetExpress.Scripts.Utils.VR.Valve.VR.InteractionSystem.Sample;
using UnityEngine;

namespace PlanetExpress.Scripts
{
    public class DraggableTile : MonoBehaviour
    {
        private TileObject _tileObject;
        private bool _isCurrentlyAttachedToHand;


        public LockToPointOrigin LockToPointOrigin;

        public void Start()
        {
            InitTileObject();
            InitManipulatorEvents();
        }

        private void InitManipulatorEvents()
        {
            ObjectManipulator o = GetComponent<ObjectManipulator>();
            o.OnManipulationStarted.AddListener(OnManipulationStarted);
            o.OnManipulationEnded.AddListener(OnManipulationEnded);
        }

        private void OnManipulationStarted(ManipulationEventData arg0)
        {
            SoundManager.Instance.PlaySound("Sons\3. SFX_Tuiles (Son Mécanique)\A) Général\1. Déplacement\1. Prendre\Déplacement_Prendre-004");
            
            if (LockToPointOrigin == null)
            {
                LockToPointOrigin = GetComponent<LockToPointOrigin>();
            }

            // Stop moving with parent and start moving with hand
            LockToPointOrigin.Move = false;


            Debug.Log("Object " + name + " was attached to hand.");
            _isCurrentlyAttachedToHand = true;
        }

        private void InitTileObject()
        {
            _tileObject = gameObject.GetComponent<TileObject>();

            if (_tileObject == null) Debug.LogError("Null tile object!");
        }

        public void OnManipulationEnded(ManipulationEventData arg0)
        {
            // Start going to point
            LockToPointOrigin.Move = true;

            Debug.Log("Object " + name + " was detached from hand.");
            _isCurrentlyAttachedToHand = false;

            // Hide the selection
            if (nearestTileSlot != null)
            {
                nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Hidden);
            }

            Debug.Log("Placing tile here...");

            HandleShopItem(GetComponent<ShopItem>());

            // Calculate the new position
            PlanetController.Instance.Place(_tileObject, nearestTileSlot);
        }

        private void HandleShopItem(ShopItem shopItem)
        {
            if (shopItem)
            {
                Debug.Log("Is shop item! Cost : " + shopItem.Cost);

                if (CurrencyController.Instance.Value < shopItem.Cost)
                {
                    Debug.LogError("Not enough gold!");
                    return;
                }

                CurrencyController.Instance.Value -= shopItem.Cost;

                // Destroy the Shop Item so other instances dont cost as much
                Destroy(shopItem);
            }
        }

        private TileSlot nearestTileSlot;

        public void Update()
        {
            if (_isCurrentlyAttachedToHand)
            {
                var nearestSlot = PlanetController.Instance.GetNearestTileSlot(_tileObject);

                if (nearestTileSlot == null)
                {
                    nearestTileSlot = nearestSlot;
                }

                var isEmpty = PlanetController.Instance.IsEmpty(nearestSlot);

                nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Hidden);
                nearestTileSlot = nearestSlot;

                if (isEmpty)
                    nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Okay);
                else
                {
                    if (_tileObject == null)
                    {
                        // Can't replace, this tile is not coming from another tile!
                        // Probably from the Shop.

                        nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Error);
                    }
                    else
                    {
                        // Will replace
                        nearestTileSlot.ArrowController.SetStatus(TileSelectionStatus.Replacing);
                    }
                }
            }
        }
    }
}