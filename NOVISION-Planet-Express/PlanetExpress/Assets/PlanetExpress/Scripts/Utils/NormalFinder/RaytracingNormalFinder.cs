using UnityEngine;

namespace PlanetExpress.Scripts.Utils.NormalFinder
{
    public class RaytracingNormalFinder : MonoBehaviour
    {
        public bool DebugRaytracingNormals = false;

        public void Update()
        {
            if (DebugRaytracingNormals)
            {
                GetHit();
            }
        }

        public Quaternion GetHit()
        {
            RaycastHit hit;

            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) * hit.distance, Color.yellow);

                // Draw lines to show the incoming "beam" and the reflection.
                Debug.DrawLine(transform.position, hit.point, Color.red);


                var dir = hit.point - (hit.point + hit.normal); //a vector pointing from pointA to pointB
                var rot = Quaternion.LookRotation(dir, Vector3.up); //calc a rotation that

                Debug.DrawRay(hit.point, dir, Color.green);


                MeshCollider meshCollider = hit.collider as MeshCollider;
                if (meshCollider == null || meshCollider.sharedMesh == null)
                    return Quaternion.identity;

                Mesh mesh = meshCollider.sharedMesh;
                Vector3[] vertices = mesh.vertices;
                int[] triangles = mesh.triangles;
                Vector3 p0 = vertices[triangles[hit.triangleIndex * 3 + 0]];
                Vector3 p1 = vertices[triangles[hit.triangleIndex * 3 + 1]];
                Vector3 p2 = vertices[triangles[hit.triangleIndex * 3 + 2]];
                Transform hitTransform = hit.collider.transform;

                p0 = hitTransform.TransformPoint(p0);
                p1 = hitTransform.TransformPoint(p1);
                p2 = hitTransform.TransformPoint(p2);
                Debug.DrawLine(p0, p1);
                Debug.DrawLine(p1, p2);
                Debug.DrawLine(p2, p0);


                Quaternion rr = Quaternion.FromToRotation(Vector3.up, p0 - p1);
                //Debug.Log("From " + p0 + " to " + p1 + " Quat " + rr.eulerAngles);

                if (DebugRaytracingNormals) Debug.Log("Did Hit " + hit.normal + ", " + rot.eulerAngles);


                return rot;
            }

            if (DebugRaytracingNormals) Debug.LogError("Fatal error! Raytracing Normal Finder " + name + " could not collide with any faces.");
            return Quaternion.identity;
        }

        public Quaternion FindNormalUp()
        {
            return GetHit();
        }
    }
}