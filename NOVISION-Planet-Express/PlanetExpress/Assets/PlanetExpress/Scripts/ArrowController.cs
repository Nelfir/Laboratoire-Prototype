using PlanetExpress.Scripts.Universe.Planet.Tiles.Shared;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots.Base;
using UnityEngine;

namespace PlanetExpress.Scripts
{
    [DefaultExecutionOrder(-999)]
    public class ArrowController : MonoBehaviour
    {
        public TileSlot TileSlot;
        public TileType TileType;

        // Start is called before the first frame update
        void Start()
        {
            PlaceArrowAtOriginAndRotation(GetNormalFaceRotation());
            CreateTileSlotComponentOnParent();
            Hide();
            SetOriginPointIsVisible(false);
        }

        private void CreateTileSlotComponentOnParent()
        {
            switch (TileType)
            {
                case TileType.Big:
                    TileSlot = gameObject.transform.parent.gameObject.AddComponent<BigTileSlot>();
                    TileSlot.SetArrowController(this);
                    break;
                case TileType.Small:
                    TileSlot = gameObject.transform.parent.gameObject.AddComponent<SmallTileSlot>();
                    TileSlot.SetArrowController(this);
                    break;
                default:
                    Debug.Log("[ArrowController] Unknown TileType!");
                    break;
            }
        }

        public void Hide()
        {
            Renderer r = GetComponentInChildren<MeshRenderer>();
            r.enabled = false;
        }
        
        public void SetOriginPointIsVisible(bool isOriginPointVisible)
        {
            GetComponent<OriginPointCreator>().Origin.SetActive(isOriginPointVisible);
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