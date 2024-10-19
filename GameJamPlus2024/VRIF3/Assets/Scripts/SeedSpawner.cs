using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    [SerializeField] private GameObject seedPrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float spawnCooldown = 60f;

    private float currentCooldown = 0f;

    private void Update()
    {
        if (currentCooldown <= spawnCooldown)
            currentCooldown += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Arrow"))
        {
            if (currentCooldown < spawnCooldown) //If in cooldown return without spawning the seeds.
                return;

            for (int i = 0; i < spawnAmount; i++) { 
            Instantiate(seedPrefab, 
                new Vector3(
                    transform.position.x + Random.Range(-spawnRadius, spawnRadius), 
                    transform.position.y, 
                    transform.position.z + Random.Range(-spawnRadius, spawnRadius)
                    ),
                Quaternion.identity
                );
            }
            currentCooldown = 0f;
        }
    }
}
