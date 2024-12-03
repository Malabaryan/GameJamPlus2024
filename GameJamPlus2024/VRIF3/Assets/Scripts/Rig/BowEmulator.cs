using UnityEngine;

public class BowEmulator : MonoBehaviour
{
    public bool Active = false;
    public GameObject Arrow;
    public Camera playerCamera;         // Assign the player's camera

    // Update is called once per frame
    void Update()
    {
        if(!Active) return;

        if (Input.GetMouseButtonDown(0))
        {
            ShootProjectile();
        }
    }
    void ShootProjectile()
    {
        if (Arrow != null && playerCamera != null)
        {
            // Spawn the projectile at the camera's position
            GameObject projectile = Instantiate(
                Arrow,
                playerCamera.transform.position + playerCamera.transform.forward, // Offset forward
                Quaternion.identity
            );

            // Add a Rigidbody component to the projectile if it doesn't have one
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = projectile.AddComponent<Rigidbody>();
            }

            // Calculate the direction based on the center of the screen
            Vector3 shootDirection = playerCamera.transform.forward;

            // Apply force to the projectile
            rb.AddForce(shootDirection * 25, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("Projectile prefab or player camera is not assigned!");
        }
    }
}
