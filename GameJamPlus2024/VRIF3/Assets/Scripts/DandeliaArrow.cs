using UnityEngine;
using BNG;

public class DandeliaArrow : MonoBehaviour
{
    [SerializeField] private GameObject arrowParticles;
    private Grabbable arrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        arrow = GetComponent<Grabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        arrowParticles.SetActive(!arrow.BeingHeld);
    }

}
