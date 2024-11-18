using UnityEngine;

public class SporeBehavior : MonoBehaviour
{
    [SerializeField] private float suctionSpeedMultiplier = 10f;
    [SerializeField] private float maxSpeed = 0.3f;
    public Transform suctionPoint;
    public bool isBeingSucked;

    private Rigidbody _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (isBeingSucked && suctionPoint != null)
        {
            Vector3 direction = (suctionPoint.position - transform.position).normalized;
            _rb.AddForce(direction * suctionSpeedMultiplier * Time.fixedDeltaTime);
            //Debug.Log("VELOCITY:" + _rb.linearVelocity.magnitude);
            if (_rb.linearVelocity.magnitude > maxSpeed)
            {
                _rb.linearVelocity = Vector3.ClampMagnitude(_rb.linearVelocity, maxSpeed);
            }
        }
    }
}
