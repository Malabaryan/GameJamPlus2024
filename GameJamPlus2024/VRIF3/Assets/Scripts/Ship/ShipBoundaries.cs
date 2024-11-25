using UnityEngine;

public class ShipBoundaries : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Get inside the zone");

        }
    }

}
