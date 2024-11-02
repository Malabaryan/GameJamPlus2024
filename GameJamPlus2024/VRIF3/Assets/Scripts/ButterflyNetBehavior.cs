using BNG;
using UnityEngine;

public class ButterflyNetBehavior : MonoBehaviour
{
    [SerializeField] private SnapZone[] snapZone;
    
    public void GrabSeed(Grabbable seed)
    {
        snapZone[0].GrabGrabbable(seed);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.transform.CompareTag("Seed"))
        {
            SeedBehavior seed = other.transform.GetComponent<SeedBehavior>();
            if(!seed.isGrabbedByNet)
            {
                seed.isGrabbedByNet = true; 
                snapZone[0].GrabGrabbable(other.transform.GetComponent<Grabbable>());
            }
            
        }
    }

}
