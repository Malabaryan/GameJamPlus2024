using UnityEngine;

public class FishesMovement : MonoBehaviour
{
    public float rotationSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, .5f);
    }
}
