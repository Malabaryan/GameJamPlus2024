using BNG;
using UnityEngine;
using UnityEngine.Events;

public class EmulatorMode : MonoBehaviour
{
    public UnityEvent EmulatorStartEvents;
    private VREmulator VREmulator;
    private bool modeSwitch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        VREmulator = GetComponent<VREmulator>();
        modeSwitch = VREmulator.HMDIsActive;
        if (!VREmulator.HMDIsActive)
        {
            EmulatorStartEvents.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (VREmulator.HMDIsActive != modeSwitch)
        {
            if (!VREmulator.HMDIsActive)
            {
                EmulatorStartEvents.Invoke();
            }
            
            
            modeSwitch = VREmulator.HMDIsActive;

        }
    }
}
