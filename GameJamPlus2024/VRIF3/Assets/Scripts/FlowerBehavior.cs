using UnityEngine;
using BNG;

public class FlowerBehavior : MonoBehaviour
{
    private Rigidbody _rb;
    private Grabbable grabbableRef;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        grabbableRef = GetComponent<Grabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbableRef.BeingHeld)
            _rb.constraints = RigidbodyConstraints.None;
    }
}
