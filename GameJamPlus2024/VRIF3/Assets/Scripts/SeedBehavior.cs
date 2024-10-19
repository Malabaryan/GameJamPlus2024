using UnityEngine;
using BNG;
using UnityEngine.Rendering;

public class SeedBehavior : MonoBehaviour
{
    public enum SeedType
    {
        Flower,
        Tulip,
        Rose
    }

    [SerializeField] SeedType type;
    [SerializeField] private float gravityScale = 0.01f;
    [SerializeField] private float maxSpeed = 0.08f;

    private Grabbable grabbableRef;
    private bool isGrabbedByNet = false;
    private Rigidbody _rb;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 1.4f, transform.position.z); //Fixed height spawn for seeds
        grabbableRef = GetComponent<Grabbable>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log(grabbableRef.BeingHeld);
        if (grabbableRef.BeingHeld && transform.parent != null) {
            transform.SetParent(null);
            isGrabbedByNet = false;
            Debug.Log("RELEASED!");
        }
    }

    private void FixedUpdate()
    {
        if (grabbableRef.BeingHeld || isGrabbedByNet) return;
        else
        {
            //transform.Translate(Vector3.up * -1 * gravityScale * Time.fixedDeltaTime, Space.World);
            _rb.AddForce(Vector3.up * -1 * gravityScale * Time.fixedDeltaTime, ForceMode.Impulse);
            if (_rb.linearVelocity.magnitude > maxSpeed)
            {
                _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, maxSpeed);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Net"))
        {
            _rb.linearDamping = 2f;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Net"))
        {
            _rb.linearDamping = 0.2f;
        }

    }

}
