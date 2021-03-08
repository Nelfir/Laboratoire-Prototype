using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots.Base;
using UnityEngine;

namespace PlanetExpress.Scripts
{
    public class ArrowController : MonoBehaviour
    {
        public TileType TileType;

        // Start is called before the first frame update
        void Start()
        {
            if (TileType == default)
            {
                Debug.LogError("[ArrowController] No TileType set for this ArrowController!");
            }

            PlaceArrowAtOriginAndRotation(GetNormalFaceRotation());
            CreateTileSlotComponentOnParent();

            SetVisible(false);
        }

        private void CreateTileSlotComponentOnParent()
        {
            TileSlot tileSlot;

            switch (TileType)
            {
                case TileType.Big:
                    tileSlot = gameObject.transform.parent.gameObject.AddComponent<BigTileSlot>();
                    tileSlot.SetArrowController(this);
                    break;
                case TileType.Small:
                    tileSlot = gameObject.transform.parent.gameObject.AddComponent<SmallTileSlot>();
                    tileSlot.SetArrowController(this);
                    break;
                default:
                    Debug.Log("[ArrowController] Unknown TileType!");
                    break;
            }
        }

        public void SetVisible(bool isVisible)
        {
            Renderer r = GetComponentInChildren<MeshRenderer>();
            r.enabled = isVisible;
        }

        private Quaternion GetNormalFaceRotation()
        {
            return GetComponent<RaytracingNormalFinder>().FindNormalRotation();
        }

        // Places the arrow facing the origin with the correct rotation
        private void PlaceArrowAtOriginAndRotation(Quaternion rotation)
        {
            OriginPointCreator originPointCreator = GetComponent<OriginPointCreator>();
            originPointCreator.SetRotation(rotation);

            transform.localPosition = new Vector3(0, 0, 0);
            transform.parent.SetParent(originPointCreator.transform, false);
            transform.position += transform.up * -10;
            transform.up = originPointCreator.transform.up;
        }
    }
}