using UnityEngine;
using BNG;

public class GrabbableGameplayItem : MonoBehaviour
{
    [SerializeField] private float heldTimer = 20f;
    [SerializeField] private SnapZone snapZone;

    private Grabbable grabbableRef;
    float currentHoldTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grabbableRef = GetComponent<Grabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbableRef.BeingHeld || snapZone.HeldItem != null)
        {
            currentHoldTime = 0f;
            return;
        }

        currentHoldTime += Time.deltaTime;

        if(currentHoldTime > heldTimer)
        {
            currentHoldTime = 0f;
            if (snapZone != null)
                snapZone.GrabGrabbable(grabbableRef);
                //snapZone.HeldItem = grabbableRef;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.name);
        if (other.transform.CompareTag("GrabbableTP") && !grabbableRef.BeingHeld)
        {
            if(snapZone != null)
                snapZone.GrabGrabbable(grabbableRef);
            //snapZone.HeldItem = grabbableRef;
        }
    }
}
