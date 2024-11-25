using BNG;
using UnityEngine;
using UnityEngine.Events;

public class EmulatorMode : MonoBehaviour
{
    public PlayerMovingPlatformSupport PlayerMovingPlatformSupport;

    private VREmulator VREmulator;
    private bool modeSwitch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VREmulator = GetComponent<VREmulator>();
        modeSwitch = VREmulator.HMDIsActive;
        PlayerMovingPlatformSupport.enabled  = !VREmulator.HMDIsActive;
        
               }
    // Update is called once per frame
    void Update()
    {
        if (VREmulator.HMDIsActive != modeSwitch)
        {
            PlayerMovingPlatformSupport.enabled = !VREmulator.HMDIsActive;


            modeSwitch = VREmulator.HMDIsActive;

        }
    }
}
