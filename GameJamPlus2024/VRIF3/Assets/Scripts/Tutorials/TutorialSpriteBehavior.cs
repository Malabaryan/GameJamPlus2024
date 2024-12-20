using BNG;
using UnityEngine;

public class TutorialSpriteBehavior : MonoBehaviour
{
    public enum TutorialType
    {
        None,
        RenderByDistance,
        RenderByGrab,
        ByTimer
    }

    [Header("General Settings")]
    [SerializeField] private TutorialType type;
    [SerializeField] private bool render = true;
    public SpriteRenderer tutorialSprite;
    private float tutorialAlpha = 1;


    [Header("Render by distance")]
    [SerializeField] private float distanceToRender = 0f;
    public GameObject shipGameObject;

    [Header("Render by Grab")]
    [SerializeField] Grabbable grabbableItem;

    [Header("Render by timer")]
    [SerializeField] private float renderTime;
    private float currentTime = 0f;

    private void Update()
    {
        switch (type) { 
            case TutorialType.RenderByDistance:
                if(shipGameObject != null)
                {
                    if(Vector3.Distance(transform.position, shipGameObject.transform.position) < distanceToRender)
                        render = true;
                    else 
                        render = false;
                }
                break;

            case TutorialType.RenderByGrab:
                if (grabbableItem != null) { 
                    render = grabbableItem.BeingHeld;
                }
                break;

            case TutorialType.ByTimer:
                if(currentTime > 0)
                    currentTime -= Time.deltaTime;
                render = currentTime > 0;
                break;
        }

        if (render)
            tutorialAlpha = Mathf.Clamp(tutorialAlpha + Time.deltaTime, 0, 1);
        else
            tutorialAlpha = Mathf.Clamp(tutorialAlpha - Time.deltaTime, 0, 1);
        tutorialSprite.color = new Color(255, 255, 255, tutorialAlpha);
    }

    public void UpdateRenderValue(bool newRender)
    {
        render = newRender;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.1f);
        Gizmos.DrawWireSphere(transform.position, distanceToRender);
    }

    public void StartTimer()
    {
        currentTime = renderTime;
    }
}
