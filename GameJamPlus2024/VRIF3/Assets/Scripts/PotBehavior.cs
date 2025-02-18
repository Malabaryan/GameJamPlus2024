using System.Collections;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    [SerializeField] GameObject turnipIndicator;
    [SerializeField] GameObject flower;
    [SerializeField] MeshFilter potFlowerMesh;
    [SerializeField] SeedBehavior.SeedType desiredSeed;
    [SerializeField] GameObject flowerVisuals;
    [SerializeField] GameObject grabbableFlower;
    [SerializeField] float spawnCooldown = 10f;

    private bool hasBeenPlanted = false;
    private SeedBehavior.SeedType seedType;
    [SerializeField] private GameObject flowerPrefab;

    private float currentCooldown = 0f;

    private void Update()
    {
        if (currentCooldown < spawnCooldown) {
            currentCooldown += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Seed") && (other.GetComponent<SeedBehavior>().type == desiredSeed || desiredSeed == SeedBehavior.SeedType.None))
        {
            //Debug.Log("Planted Seed");
            hasBeenPlanted = true;
            turnipIndicator.SetActive(true);
            SeedBehavior seed = other.GetComponent<SeedBehavior>();
            seedType = seed.type;
            flowerPrefab = seed.grabbableFlowerPrefab;
            potFlowerMesh.mesh = seed.flowerMesh; //Change visual animation flower mesh
            Destroy(seed.gameObject);
        }

        if (other.transform.CompareTag("Water") && hasBeenPlanted && currentCooldown > spawnCooldown)
        {
            Debug.Log("Triggered water");
            currentCooldown = 0;
            flower.SetActive(true);
            flower.GetComponent<Animator>().Play("Flower");
            StartCoroutine(SpawnGrabbableFlower());
        }
    }

    IEnumerator SpawnGrabbableFlower()
    {
        yield return new WaitForSeconds(1f);
        flower.SetActive(false);
        var instFlower = Instantiate(flowerPrefab, grabbableFlower.transform.position, grabbableFlower.transform.rotation);
        instFlower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        instFlower.transform.SetParent(transform, true);
        instFlower.SetActive(true);
    }
}
