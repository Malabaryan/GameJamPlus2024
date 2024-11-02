using UnityEngine;
using BNG;
using UnityEngine.Rendering;

public class SeedBehavior : MonoBehaviour
{
    public enum SeedType
    {
        Flower,
        Heliconia,
        Cala
    }

    public SeedType type;
    [SerializeField] private GameObject flowerFrefab;
    [SerializeField] private float gravityScale = 0.01f;
    [SerializeField] private float heightOffset = 0.7f;
    [SerializeField] private float speedMultiplier = 1.5f;
    [SerializeField] private float maxSpeed = 0.08f;
    [SerializeField] private float lifetime;
    [SerializeField] private float minHeight = -9;
    [SerializeField] private GameObject particles;


    private Grabbable grabbableRef;
    public bool isGrabbedByNet = false;
    private Rigidbody _rb;
    private float gravityMultiplier;
    private float currentLifetime = 0f;

    private void Start()
    {
        //transform.position = new Vector3(transform.position.x, 1.4f , transform.position.z); //Fixed height spawn for seeds + Random.Range(-heightOffset, heightOffset)
        grabbableRef = GetComponent<Grabbable>();
        _rb = GetComponent<Rigidbody>();
        gravityMultiplier = Random.Range(-gravityMultiplier, gravityMultiplier);
        transform.localEulerAngles = new Vector3(-90, 0, 0);
    }

    void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime > lifetime)
        {
            Destroy(gameObject);
        }
        if (this.transform.position.y < minHeight) {
            Destroy(gameObject);

        }

        if(grabbableRef.BeingHeld || isGrabbedByNet)
            particles.SetActive(false);
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
