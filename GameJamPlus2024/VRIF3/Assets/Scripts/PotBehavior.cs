using System;
using System.Collections;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    public GameObject turnipIndicator;
    public GameObject flower;
    public MeshFilter potFlowerMesh;
    public SeedBehavior.SeedType desiredSeed;
    public GameObject flowerVisuals;
    public GameObject grabbableFlower;
    public float spawnCooldown = 10f;

    [NonSerialized]
    public bool hasBeenPlanted = false;
    [NonSerialized]
    public SeedBehavior.SeedType SeedType;
    public GameObject flowerPrefab;

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
            SeedType = seed.type;
            flowerPrefab = seed.grabbableFlowerPrefab;
            potFlowerMesh.mesh = seed.flowerMesh; //Change visual animation flower mesh
            Destroy(seed.gameObject);
        }

        if (other.transform.CompareTag("Water") && hasBeenPlanted && currentCooldown > spawnCooldown)
        {
            Debug.Log("Triggered water");
            turnipIndicator.SetActive(false);
            currentCooldown = 0;
            flower.SetActive(true);
            flower.GetComponent<Animator>().Play("Flower");
            StartCoroutine(SpawnGrabbableFlower());
        }
    }

    public IEnumerator SpawnGrabbableFlower()
    {
        yield return new WaitForSeconds(1f);
        flower.SetActive(false);
        var instFlower = Instantiate(flowerPrefab, grabbableFlower.transform.position, grabbableFlower.transform.rotation);
        instFlower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        instFlower.transform.SetParent(transform, true);
        instFlower.SetActive(true);
    }


}
