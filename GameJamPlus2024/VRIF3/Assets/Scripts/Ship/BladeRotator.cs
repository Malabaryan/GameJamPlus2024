using UnityEngine;

public class BladeRotator : MonoBehaviour
{
    // Speed of rotation (can be positive or negative)
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotate the object around its center
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
