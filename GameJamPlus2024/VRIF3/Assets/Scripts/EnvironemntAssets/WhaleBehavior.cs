using UnityEngine;

public class WhaleBehavior : MonoBehaviour
{
    [Header("Animation Sound effects")]
    [SerializeField] float playableDistance = 200f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] whaleSounds;

    private int index = 0;

    public void PlayWhaleSFX()
    {
        Transform ship = GameObject.Find("Ship").transform;
        if (Vector3.Distance(transform.position, ship.position) > playableDistance) return; //Only play when close to the Whale
        
        audioSource.clip = whaleSounds[index];
        audioSource.Play();
        index++;
        if(index >= whaleSounds.Length)
            index = 0;
    }

}
