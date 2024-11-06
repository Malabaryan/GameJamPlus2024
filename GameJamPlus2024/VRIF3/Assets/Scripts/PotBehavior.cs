using System.Collections;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    [SerializeField] GameObject turnipIndicator;
    [SerializeField] GameObject flower;
    public Mesh testingMesh;
    [SerializeField] MeshFilter potFlowerMesh;
    [SerializeField] SeedBehavior.SeedType desiredSeed;
    [SerializeField] GameObject flowerVisuals;
    //[SerializeField] GameObject[] flowerVisuals;
    [SerializeField] GameObject grabbableFlower;
    //[SerializeField] GameObject grabbableCala;
    //[SerializeField] GameObject grabbableHeliconia;
    [SerializeField] float spawnCooldown = 10f;

    private bool hasBeenPlanted = false;
    private SeedBehavior.SeedType seedType;
    [SerializeField] private GameObject flowerPrefab;

    private float currentCooldown = 0f;

    private void Start()
    {
        potFlowerMesh.mesh = testingMesh;
    }

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
            currentCooldown = 0;
            flower.SetActive(true);
            flower.GetComponent<Animator>().Play("Flower");
            //switch (seedType)
            //{
            //    case SeedBehavior.SeedType.Flower:
            //        flowerVisuals[0].SetActive(true);
            //        break;

            //    case SeedBehavior.SeedType.Heliconia:
            //        flowerVisuals[1].SetActive(true);
            //        break;

            //    case SeedBehavior.SeedType.Cala:
            //        flowerVisuals[2].SetActive(true);
            //        break;
            //}
            StartCoroutine(SpawnGrabbableFlower());
        }
    }

    IEnumerator SpawnGrabbableFlower()
    {
        yield return new WaitForSeconds(1f);
        flower.SetActive(false);
        var instFlower = Instantiate(flowerPrefab, grabbableFlower.transform.position, grabbableFlower.transform.rotation);
        instFlower.transform.SetParent(transform, true);
        instFlower.SetActive(true);
        //switch(seedType)
        //{
        //    case SeedBehavior.SeedType.Flower:
        //        var instFlower = Instantiate(grabbableFlower, grabbableFlower.transform.position, grabbableFlower.transform.rotation);
        //        instFlower.transform.SetParent(transform, true);
        //        instFlower.SetActive(true);
        //        //grabbableFlower.SetActive(true);
        //        break;

        //    case SeedBehavior.SeedType.Heliconia:
        //        var instFlower2 = Instantiate(grabbableHeliconia, grabbableHeliconia.transform.position, grabbableHeliconia.transform.rotation);
        //        instFlower2.SetActive(true);
        //        instFlower2.transform.SetParent(transform, true);
        //        //grabbableHeliconia.SetActive(true);
        //        break;

        //    case SeedBehavior.SeedType.Cala:
        //        var instFlower3 = Instantiate(grabbableCala, grabbableCala.transform.position, grabbableCala.transform.rotation);
        //        instFlower3.SetActive(true);
        //        instFlower3.transform.SetParent(transform, true);
        //        //grabbableCala.SetActive(true);
        //        break;
        //}

    }
}
