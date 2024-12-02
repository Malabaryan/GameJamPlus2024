using UnityEngine;
using BNG;

public class FlowerBehavior : MonoBehaviour
{
    public SeedBehavior.SeedType flowerType = SeedBehavior.SeedType.None;
    [SerializeField] Vector3 desiredRotation = Vector3.zero;
    private Rigidbody _rb;
    private Grabbable grabbableRef;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        grabbableRef = GetComponent<Grabbable>();
        transform.localEulerAngles = desiredRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbableRef.BeingHeld)
            _rb.constraints = RigidbodyConstraints.None;
    }
}
