using System;
using System.Linq;
using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots.Base;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace PlanetExpress.Scripts.Shop
{
    public class ShopItem : MonoBehaviour
    {
        public string Name;
        public int Cost;
        public TileType TileType;

        private TileObject _tileObject;
        private bool _isCurrentlyAttachedToHand;

        public void Start()
        {
            InitTileObject();
            InitInfoCanvas();
            InitHandleHoverEvents();
        }

        private void InitTileObject()
        {
            switch (TileType)
            {
                case TileType.Small:
                    _tileObject = gameObject.AddComponent<SmallTileObject>();
                    break;
                case TileType.Big:
                    _tileObject = gameObject.AddComponent<BigTileObject>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void InitInfoCanvas()
        {
            // Creates the UI with the name and cost text
            GameObject canvasInfo = Resources.Load<GameObject>("CanvasShopItemDescription");
            GameObject o = Instantiate(canvasInfo, gameObject.transform);

            ShopItemInfo shopItemInfo = o.GetComponent<ShopItemInfo>();
            shopItemInfo.SetInfo(Name, Cost);
        }

        private void InitHandleHoverEvents()
        {
            // Register the grab events
            InteractableHoverEvents interactableHoverEvents = GetComponent<InteractableHoverEvents>();

            interactableHoverEvents.onAttachedToHand.AddListener(() =>
            {
                Debug.Log("Object " + Name + " was attached to hand.");
                _isCurrentlyAttachedToHand = true;
            });
            interactableHoverEvents.onDetachedFromHand.AddListener(() =>
            {
                Debug.Log("Object " + Name + " was detached from hand.");
                _isCurrentlyAttachedToHand = false;
            });
        }

        private TileSlot nearestTileSlot;


        public void Update()
        {
            if (_isCurrentlyAttachedToHand)
            {
                var nearestSlot = PlanetController.Instance.GetNearestPlaceableTileSlots(_tileObject);

                Debug.Log("There is " + nearestSlot.Count + " possible slot(s).");

                var newNearestTileSlot = nearestSlot.FirstOrDefault();

                if (newNearestTileSlot == null)
                {
                    Debug.Log("Null nearest slot.");
                    return;
                }

                if (newNearestTileSlot != nearestTileSlot)
                {
                    nearestTileSlot.SetArrowVisible(false);

                    nearestTileSlot = newNearestTileSlot;
                    nearestTileSlot.SetArrowVisible(true);
                }
            }
        }
    }
}