using UnityEngine;
using UnityEngine.Events;

public class DandeliaBeacon : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Material beaconMaterial;
    [SerializeField] private MeshRenderer beaconMesh;
    [SerializeField] private float riseSpeed = 1f;
    [SerializeField] private float desiredHeight = 0f;
    [SerializeField] private ParticleSystem beaconParticleSystem;
    [SerializeField] private bool isFirstBeacon = false;

    [Header("Beacon Activated")]
    public UnityEvent beaconActivated;

    private bool shouldRise = false;

    private AudioSource beaconAudioSource;

    private void Start()
    {
        beaconAudioSource = GetComponent<AudioSource>();

        //Hide beacon
        transform.position = transform.position + new Vector3(0f, -105f, 0f);

        if(isFirstBeacon) Rise();
    }

    void Update()
    {
        if (!shouldRise)
            return;

        transform.Translate(Vector3.up * Time.deltaTime * riseSpeed);
        if(transform.position.y >= desiredHeight)
        {
            //ActivateBeacon();
            shouldRise = false;
            beaconParticleSystem.Stop();
            beaconAudioSource.Stop();
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
        beaconParticleSystem.Play();
        beaconAudioSource.Play();
    }
}
