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
    [SerializeField] private float heightOffset = 0.7f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float maxSpeed = 0.08f;
    [SerializeField] private float lifetime;
    

    private Grabbable grabbableRef;
    private bool isGrabbedByNet = false;
    private Rigidbody _rb;
    private float gravityMultiplier;
    private float currentLifetime = 0f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, 1.4f + Random.Range(-heightOffset, heightOffset), transform.position.z); //Fixed height spawn for seeds
        grabbableRef = GetComponent<Grabbable>();
        _rb = GetComponent<Rigidbody>();
        gravityMultiplier = Random.Range(-gravityMultiplier, gravityMultiplier);
    }

    void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime > lifetime)
        {
            Destroy(gameObject);
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
            //Debug.Log("Catched");
            _rb.linearDamping = 2f;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Net"))
        {
            //Debug.Log("Released");
            _rb.linearDamping = 0.2f;
        }

    }

}
