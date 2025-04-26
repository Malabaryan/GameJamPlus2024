using UnityEngine;

public class BiomeSwap : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip newBiomeTrack;

    [Header("Visuals Settings")]
    [SerializeField] AudioSource newBiomeVisuals;

    private bool doSwap = false;
    private bool volumeDown = true;
    private void OnTriggerEnter(Collider other)
    {
        if (audioSource.clip == newBiomeTrack) return;
        Debug.Log("SWITCH");
        //If ship collides with this object make a transition animation
        doSwap = true;
        volumeDown = true;
    }

    private void Update()
    {
        //Swap music
        if(doSwap)
        {
            //Volume down and swap music
            if(volumeDown)
                audioSource.volume -= Time.deltaTime * 0.1f;
            else if(volumeDown == false && audioSource.volume < 0.5f)
                audioSource.volume += Time.deltaTime * 0.2f;
            else
                doSwap = false; //Should do the swap when clip has changed

            if (audioSource.volume <= 0) 
            {
                audioSource.clip = newBiomeTrack;
                audioSource.Play();
                volumeDown = false;
            }
        }
    }
}
