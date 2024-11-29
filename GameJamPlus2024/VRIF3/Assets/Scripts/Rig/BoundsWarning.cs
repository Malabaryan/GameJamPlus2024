using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BoundsWarning : MonoBehaviour
{

    [Tooltip("How fast to fade in / out")]
    public float FadeInSpeed = 6f;
    public Camera Camera;
    public float FadeOutSpeed = 6f;
    public Canvas Canvas;
    public Animator Animator;

    IEnumerator fadeRoutine;

    /// <summary>
    /// Fade from transparent to solid color
    /// </summary>
    public virtual void DoFadeIn()
    {
        gameObject.SetActive(true);
        Canvas.worldCamera = Camera;

        Animator.SetTrigger("FadeIn");
    }

    /// <summary>
    /// Fade from solid color to transparent
    /// </summary>
    public virtual void DoFadeOut()
    {
        Canvas.worldCamera = Camera;
        Animator.SetTrigger("FadeOut");


    }
}
      
    


