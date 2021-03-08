using PlanetExpress.Scripts.Universe.Planet.Shared;
using PlanetExpress.Scripts.Universe.Planet.TileSlots;
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
                Destroy(this);
            }

            PlaceArrowAtOriginAndRotation(GetNormalFaceRotation());
            CreateTileSlotComponentOnParent();

            Hide();
        }

        private void CreateTileSlotComponentOnParent()
        {
            switch (TileType)
            {
                case TileType.Big:
                    gameObject.transform.parent.gameObject.AddComponent<BigTileSlot>();
                    break;
                case TileType.Small:
                    gameObject.transform.parent.gameObject.AddComponent<SmallTileSlot>();
                    break;
                default:
                    Debug.Log("[ArrowController] Unknown TileType!");
                    break;
            }
        }

        private void Hide()
        {
            Renderer r = GetComponentInChildren<MeshRenderer>();
            r.enabled = false;
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