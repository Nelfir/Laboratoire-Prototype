using UnityEngine;

/// <summary>
/// Constantly rotate an object along the x, y and z axies.
/// </summary>
public class ConstantRotation : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(1, 1, 1));
    }
}