using UnityEngine;
using BNG;

public class FlowerBehavior : MonoBehaviour
{
    public SeedBehavior.SeedType flowerType = SeedBehavior.SeedType.None;
    [SerializeField] private Vector3 desiredRotation = Vector3.zero;
    [SerializeField] private bool isInitialFlower = false;
    private Rigidbody _rb;
    private Grabbable grabbableRef;
    private Vector3 originalPosition;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        grabbableRef = GetComponent<Grabbable>();
        transform.localEulerAngles = desiredRotation;
        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbableRef.BeingHeld)
            _rb.constraints = RigidbodyConstraints.None;

        if (!isInitialFlower) return;
        if(transform.position.y < 3f)
        {
            transform.localPosition = originalPosition;
            _rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.localEulerAngles = desiredRotation;
        }
    }
}
