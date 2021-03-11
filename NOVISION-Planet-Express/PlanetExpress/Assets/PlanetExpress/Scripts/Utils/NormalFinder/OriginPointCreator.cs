using UnityEngine;

namespace PlanetExpress.Scripts.Utils.NormalFinder
{
    /// <summary>
    /// Creates a point at the origin of the mesh.
    /// </summary>
    public class OriginPointCreator : MonoBehaviour
    {
        [HideInInspector] public GameObject Origin;

        public float RotationOffset;

        private GameObject _prefabSphereRotation;

        public Transform TileOriginTransform;

        // Start is called before the first frame update
        void Awake()
        {
            if (_prefabSphereRotation == null) _prefabSphereRotation = Resources.Load<GameObject>("TileSelection/TileSelection");

            Origin = Instantiate(_prefabSphereRotation);
            Origin.transform.localScale = new Vector3(10, 10, 10);
            Origin.transform.position = Vector3.zero;
            Origin.transform.SetParent(transform.parent, false);
        }

        public void SetRotation(Quaternion rotation)
        {
            Origin.transform.rotation = rotation;
            Origin.transform.eulerAngles -= new Vector3(90, 0, 0);

            GameObject tuileVide = Origin.GetComponentInChildren<TuileVideOriginale>().gameObject;
            tuileVide.transform.Rotate(new Vector3(0, RotationOffset, 0));

            TileOriginTransform = tuileVide.transform;
        }
    }
}