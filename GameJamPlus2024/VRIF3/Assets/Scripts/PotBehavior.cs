using System.Collections;
using UnityEngine;

public class PotBehavior : MonoBehaviour
{
    [SerializeField] GameObject turnipIndicator;
    [SerializeField] GameObject flower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Seed")) {
            Debug.Log("Planted Seed");
            turnipIndicator.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("Water"))
        {
            flower.SetActive(true);
        }
    }
}
