using System;
using System.Linq;
using PlanetExpress.Scripts.Currency;
using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileObjects.Base;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots.Base;
using PlanetExpress.Scripts.Utils.VR;
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
            shopItemInfo.SetInfo(Name, Cost, TileType);
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


                PlacedResult placedResult = PlanetController.Instance.CanBePlaced(_tileObject, nearestTileSlot);

                switch (placedResult)
                {
                    case PlacedResult.OK:
                    {
                        Debug.Log("Placing tile here...");
                        Debug.Log("Cost : " + Cost);

                        if (CurrencyController.Instance.Money < Cost)
                        {
                            Debug.LogError("Not enough gold!");
                            return;
                        }
                        
                        CurrencyController.Instance.UpdateMoney(-Cost);

                        LockToPointOrigin lockToPointOrigin = GetComponent<LockToPointOrigin>();
                        lockToPointOrigin.snapTo = nearestTileSlot.transform;

                        break;
                    }
                    case PlacedResult.NotEmpty:
                        Debug.LogError("Can't place this ShopItem here. The slot is not empty!");
                        break;
                    case PlacedResult.TypeMismatch:
                        Debug.LogError("Can't place this ShopItem here. The type is mismatched!");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }

        private TileSlot nearestTileSlot;

        public void Update()
        {
            if (_isCurrentlyAttachedToHand)
            {
                var nearestSlot = PlanetController.Instance.GetNearestPlaceableTileSlots(_tileObject, true);

                Debug.Log("There is " + nearestSlot.Count + " possible slot(s).");

                var newNearestTileSlot = nearestSlot.FirstOrDefault();

                if (newNearestTileSlot == null)
                {
                    Debug.Log("Null nearest slot.");
                    return;
                }

                if (nearestTileSlot == null)
                {
                    nearestTileSlot = newNearestTileSlot;
                    nearestTileSlot.SetArrowVisible(true);
                }
                else
                {
                    nearestTileSlot.SetArrowVisible(false);
                    nearestTileSlot = newNearestTileSlot;
                    nearestTileSlot.SetArrowVisible(true);
                }
            }
        }
    }
}