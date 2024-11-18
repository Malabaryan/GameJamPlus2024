using UnityEngine;
using BNG;

public class VacuumBehavior : MonoBehaviour
{
    [SerializeField] private Grabbable vacuumHandle;
    [SerializeField] private Transform suctionPoint;
    [SerializeField] private SnapZone seedSnapZone;
    [SerializeField] private float catchDistance = 0.15f;
    [SerializeField] private BoxCollider suctionCollider;
    [SerializeField] private bool isTurnedOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //suctionCollider.enabled = vacuumHandle.BeingHeld && isTurnedOn;
        //Disabling the suction collider didn't cast on trigger exit, so had to improvise :P
        suctionCollider.size = isTurnedOn ? new Vector3(0.5f, 1.21f, 0.5f) : Vector3.zero;
    }

    public void TogglePower()
    {
        isTurnedOn = !isTurnedOn;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Seed") && seedSnapZone.HeldItem == null)
        {
            SporeBehavior spore = other.GetComponent<SporeBehavior>();
            spore.suctionPoint = suctionPoint;
            spore.isBeingSucked = true;

            if(Vector3.Distance(spore.transform.position, suctionPoint.transform.position) < catchDistance)
            {
                seedSnapZone.GrabGrabbable(other.GetComponent<Grabbable>());
                spore.suctionPoint = null;
                spore.isBeingSucked = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Seed"))
        {
            SporeBehavior spore = other.GetComponent<SporeBehavior>();
            spore.suctionPoint = null;
            spore.isBeingSucked = false;
        }
            
    }
}
