using UnityEngine;
using BNG;

public class WateringCanBehavior : MonoBehaviour
{
    [SerializeField] private GameObject waterCollider;
    private Grabbable grabbableRef;
    private void Start()
    {
        grabbableRef = GetComponent<Grabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        //if () { 
            waterCollider.SetActive(grabbableRef.BeingHeld && transform.localEulerAngles.x > 30f && transform.localEulerAngles.x < 70f);
        //}
    }
}
