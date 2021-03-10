using PlanetExpress.Scripts.Universe.Planet.Tiles.TileSlots;
using UnityEngine;

namespace PlanetExpress.Scripts.Utils.NormalFinder
{
    [DefaultExecutionOrder(-999)]
    public class ArrowController : MonoBehaviour
    {
        [HideInInspector] public TileSlot TileSlot;

        [HideInInspector] public OriginPointCreator OriginPointCreator;

        // Start is called before the first frame update
        void Start()
        {
            OriginPointCreator = GetComponent<OriginPointCreator>();

            PlaceArrowAtOriginAndRotation(GetArrowUpRotation());
            CreateTileSlotComponentOnParent();

            Hide();
            SetOriginPointIsVisible(false);
        }

        private void CreateTileSlotComponentOnParent()
        {
            TileSlot = gameObject.transform.parent.gameObject.AddComponent<TileSlot>();
            TileSlot.SetArrowController(this);
        }

        public void Hide()
        {
            Renderer r = GetComponentInChildren<MeshRenderer>();
            r.enabled = false;
        }

        public void SetOriginPointIsVisible(bool isOriginPointVisible)
        {
            OriginPointCreator.Origin.SetActive(isOriginPointVisible);
        }

        private Quaternion GetArrowUpRotation()
        {
            return GetComponent<RaytracingNormalFinder>().FindNormalUp();
        }

        // Places the arrow facing the origin with the correct rotation
        private void PlaceArrowAtOriginAndRotation(Quaternion arrowFacingUp)
        {
            OriginPointCreator.SetRotation(arrowFacingUp);

            transform.localPosition = new Vector3(0, 0, 0);
            transform.parent.SetParent(OriginPointCreator.transform, false);
            transform.position += transform.up * -10;
            transform.up = OriginPointCreator.transform.up;

            //transform.rotation = shapeRotation;
        }
    }
}