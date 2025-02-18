using BNG;
using UnityEngine;

public class FireFlowerBehavior : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Arrow"))
        {
            Debug.Log("Cambiando flecha");
            Arrow arrow = other.transform.GetComponent<Arrow>();
            if(arrow != null )
                arrow.TurnOnFire();
        }
    }
}
