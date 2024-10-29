using UnityEngine;
using UnityEngine.UI;

public class ShipCover : MonoBehaviour
{
    public Animator Animator;

    private bool toggle = false;

    public void ToggleAnimation()
    {
        toggle = !toggle;
        if (toggle)
        {
            Animator.SetTrigger("Up");
        }
        else
        {
            Animator.SetTrigger("Down");
        }
    }
}
