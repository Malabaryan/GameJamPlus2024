using System.Collections;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    [SerializeField] GameObject turnipIndicator;
    [SerializeField] GameObject flower;

    private bool hasBeenPlanted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Seed")) {
            //Debug.Log("Planted Seed");
            hasBeenPlanted = true;
            turnipIndicator.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("Water") && hasBeenPlanted)
        {
            flower.SetActive(true);
        }
    }
}
