using System.Collections;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    [SerializeField] GameObject turnipIndicator;
    [SerializeField] GameObject flower;
    [SerializeField] SeedBehavior.SeedType desiredSeed;
    [SerializeField] GameObject[] flowerVisuals;
    [SerializeField] GameObject grabbableFlower;
    [SerializeField] GameObject grabbableCala;
    [SerializeField] GameObject grabbableHeliconia;

    private bool hasBeenPlanted = false;
    private SeedBehavior.SeedType seedType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Seed") && other.GetComponent<SeedBehavior>().type == desiredSeed) {
            //Debug.Log("Planted Seed");
            hasBeenPlanted = true;
            turnipIndicator.SetActive(true);
            seedType = other.GetComponent<SeedBehavior>().type;
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("Water") && hasBeenPlanted)
        {
            flower.SetActive(true);
            switch (seedType)
            {
                case SeedBehavior.SeedType.Flower:
                    flowerVisuals[0].SetActive(true);
                    break;

                case SeedBehavior.SeedType.Heliconia:
                    flowerVisuals[1].SetActive(true);
                    break;

                case SeedBehavior.SeedType.Cala:
                    flowerVisuals[2].SetActive(true);
                    break;
            }
            StartCoroutine(SpawnGrabbableFlower());
        }
    }

    IEnumerator SpawnGrabbableFlower()
    {
        yield return new WaitForSeconds(1f);
        flower.SetActive(false);
        switch(seedType)
        {
            case SeedBehavior.SeedType.Flower:
                grabbableFlower.SetActive(true);
                break;

            case SeedBehavior.SeedType.Heliconia:
                grabbableHeliconia.SetActive(true);
                break;

            case SeedBehavior.SeedType.Cala:
                grabbableCala.SetActive(true);
                break;
        }
            
    }
}
