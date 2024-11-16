using UnityEngine;
using BNG;

public class VacuumBehavior : MonoBehaviour
{
    [SerializeField] Grabbable vacuumHandle;
    [SerializeField] bool isTurnedOn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePower()
    {
        isTurnedOn = !isTurnedOn;
    }
}
