using UnityEngine;

namespace DefaultNamespace
{
    public class ArrowController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            PlaceArrowAtOriginAndRotation(GetNormalFaceRotation());
            Hide();
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

            //transform.rotation = originPointCreator.transform.rotation;
        }
    }
}