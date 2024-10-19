using UnityEngine;
using BNG;

public class WateringCanBehavior : MonoBehaviour
{
    [SerializeField] private GameObject waterCollider;
    private Grabbable grabbableRef;
    private void Start()
    {
        grabbableRef = GetComponent<Grabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbableRef.BeingHeld) { 
            waterCollider.SetActive(true);
        }
        Debug.Log("ANGLES: " + transform.localEulerAngles);
    }
}
