using FlatKit;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BiomeSwap : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip newBiomeTrack;

    [Header("Visuals Settings")]
    [SerializeField] FlatKitFog FogSettings;
    [SerializeField] FogSettings oldFog;
    [SerializeField] FogSettings newFog;

    private float oldFogDistanceFogIntensity = 0f;
    private float oldFogHeightFogIntensity = 0f;
    private float newdFogDistanceFogIntensity = 0f;
    private float newFogHeightFogIntensity = 0f;

    private bool doSwapMusic = false;
    private bool volumeDown = true;

    private bool doSwapFog = false;
    private bool fogDown = true;

    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (audioSource.clip == newBiomeTrack) return;
        Debug.Log("SWITCH");
        //If ship collides with this object make a transition animation
        doSwapMusic = true;
        volumeDown = true;

        //Swap biome
        FogSettings.settings = newFog;
        FogSettings.settings.useDistance = false;
        FogSettings.settings.useHeight = false;
        FogSettings.settings.useDistance = true;
        FogSettings.settings.useHeight = true;
        //doSwapFog = true;
        //fogDown = true;
        //oldFog = FogSettings.settings;
        //oldFogDistanceFogIntensity = oldFog.distanceFogIntensity;
        //oldFogHeightFogIntensity = oldFog.heightFogIntensity;
        //newdFogDistanceFogIntensity = newFog.distanceFogIntensity;
        //newFogHeightFogIntensity = newFog.heightFogIntensity;
    }

    private void Update()
    {
        //Swap music
        if(doSwapMusic)
        {
            //Volume down and swap music
            if(volumeDown)
                audioSource.volume -= Time.deltaTime * 0.1f;
            else if(volumeDown == false && audioSource.volume < 0.5f)
                audioSource.volume += Time.deltaTime * 0.2f;
            else
                doSwapMusic = false; //Should do the swap when clip has changed

            if (audioSource.volume <= 0) 
            {
                audioSource.clip = newBiomeTrack;
                audioSource.Play();
                volumeDown = false;
            }
        }

        //Swap biome smooth transition
        //Si funciona pero no se nota ingame
        //if(doSwapFog)
        //{
        //    if(fogDown)
        //    {
        //        //Intensity down
        //        if(oldFog.distanceFogIntensity > 0f) oldFog.distanceFogIntensity -= Time.deltaTime * 0.1f;
        //        if (oldFog.heightFogIntensity > 0f) oldFog.heightFogIntensity -= Time.deltaTime * 0.1f;
        //        if (oldFog.distanceFogIntensity <= 0 && oldFog.heightFogIntensity <= 0)
        //        {
        //            //Do the magic swap current fog lowered intensity, set new fog intensity to 0 to start going up
        //            newFog.distanceFogIntensity = 0f;
        //            newFog.heightFogIntensity = 0f;
        //            FogSettings.settings = newFog;
        //            oldFog.distanceFogIntensity = oldFogDistanceFogIntensity;
        //            oldFog.heightFogIntensity = oldFogHeightFogIntensity;
        //            fogDown = false;
        //        }
        //    }
        //    else
        //    {
        //        //Intensity up
        //        if (newFog.distanceFogIntensity < newdFogDistanceFogIntensity) newFog.distanceFogIntensity += Time.deltaTime * 0.1f;
        //        if (newFog.heightFogIntensity < newFogHeightFogIntensity) newFog.heightFogIntensity += Time.deltaTime * 0.1f;
        //        if (newFog.distanceFogIntensity >= newdFogDistanceFogIntensity && 
        //            newFog.heightFogIntensity >= newFogHeightFogIntensity) doSwapFog = false;
        //    }
        //}
    }
}
