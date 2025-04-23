using BNG;
using UnityEngine;
using BNG;

public class ArrowFlowerInteraction : MonoBehaviour
{
    public ArrowType arrowType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Arrow"))
        {
            Debug.Log("Cambiando flecha");
            Arrow arrow = other.transform.GetComponent<Arrow>();
            if (arrow != null)
            {
                switch (arrowType)
                {
                    case ArrowType.Fire:
                        arrow.TurnOnFire();
                        break;
                    case ArrowType.Bomb:
                        arrow.EnableBomb();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
