using UnityEngine;

public class ShipBoundaries : MonoBehaviour
{
    public BoundsWarning BoundsWarning;
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            BoundsWarning.DoFadeIn();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            BoundsWarning.DoFadeOut();

        }
    }
}
