using UnityEngine;
using UnityEngine.Events;

public class DandeliaBeacon : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Material beaconMaterial;
    [SerializeField] private MeshRenderer beaconMesh;
    [SerializeField] private float riseSpeed = 1f;
    [SerializeField] private float desiredHeight = 0f;

    [Header("Beacon Activated")]
    public UnityEvent beaconActivated;

    private bool shouldRise = false;

    void Update()
    {
        if (!shouldRise)
            return;

        transform.Translate(Vector3.up * Time.deltaTime * riseSpeed);
        if(transform.position.y >= desiredHeight)
        {
            ActivateBeacon();
            shouldRise = false;
        }       
    }

    public void ActivateBeacon()
    {
        beaconMesh.material = beaconMaterial;
        beaconActivated.Invoke();
    }

    public void Rise()
    {
        shouldRise = true;
    }
}
