using BNG;
using UnityEngine;

public class DeliverBoxBehavior : MonoBehaviour
{
    [SerializeField] private SnapZone bouquetSlot;
    [SerializeField] SeedBehavior.SeedType desiredFlower;
    [SerializeField] private Transform lidTransform;

    private bool lidClosed = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateDesiredFlower(SeedBehavior.SeedType newDesiredFlower)
    {
        desiredFlower = newDesiredFlower;
    }
    public void CheckDesiredFlower()
    {
        if (bouquetSlot.HeldItem != null)
        {
            if (desiredFlower == bouquetSlot.HeldItem.transform.GetComponent<BouqueteBehavior>().flowerType)
            {
                Debug.Log("FLOR DESEADA");
            }
            else
            {
                Debug.Log("FLOR NO DESEADA");
            }
        }
    }

    public void CloseTheLid()
    {
        lidClosed = !lidClosed;
        lidTransform.localEulerAngles = new Vector3(lidClosed ? 0 : 130f, 0, 0);
    }
}
