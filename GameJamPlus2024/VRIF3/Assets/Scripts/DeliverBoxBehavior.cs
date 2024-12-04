using BNG;
using UnityEngine;

public class DeliverBoxBehavior : MonoBehaviour
{
    [Header("Deliver Box Properties")]
    [SerializeField] private SnapZone bouquetSlot;
    [SerializeField] SeedBehavior.SeedType desiredFlower;
    [SerializeField] private Transform lidTransform;

    [Header("Deliver Button Materials")]
    [SerializeField] private GameObject buttonMesh;
    [SerializeField] private Material wrongMaterial;
    [SerializeField] private Material correctMaterial;

    private bool lidClosed = false;

    void Start()
    {
        buttonMesh.GetComponent<MeshRenderer>().material = wrongMaterial;
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
                buttonMesh.GetComponent<MeshRenderer>().material = correctMaterial;
            }
            else
            {
                Debug.Log("FLOR NO DESEADA");
                buttonMesh.GetComponent<MeshRenderer>().material = wrongMaterial;
            }
        }
    }

    public void CloseTheLid()
    {
        lidClosed = !lidClosed;
        lidTransform.localEulerAngles = new Vector3(lidClosed ? 0 : 130f, 0, 0);
    }
}
