using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ShipAnchor : MonoBehaviour
{
    public UnityEvent AnchorTrigger;
    public float TriggerHeight;

    private bool hasTriggered;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        startPosition = this.transform.localPosition;
        startRotation = this.transform.rotation;

    }
    void Update()
    {
        if (transform.position.y < TriggerHeight && !hasTriggered)
        {
            AnchorTrigger.Invoke();
            hasTriggered = true;
            this.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    public void Reset()
    {
        this.GetComponent<Rigidbody>().useGravity = true;
        hasTriggered = false;
        transform.localPosition = startPosition;
        transform.rotation = startRotation;

    }
}
