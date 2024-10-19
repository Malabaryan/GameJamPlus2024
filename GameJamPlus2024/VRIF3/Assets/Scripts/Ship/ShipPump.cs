using BNG;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShipPump : MonoBehaviour
{
    public Lever lever;
    public float MaxValue;
    public float MinValue;
    public float AddAmount;
    public float DecayValue;
    public float CurrentValue;

    private bool lastState;

    public void Start()
    {
        CurrentValue = MinValue;
        StartCoroutine(ReduceValue());

    }
    public void CrankPump(bool state)
    {
        if (lastState != state) {
            CurrentValue = CurrentValue + AddAmount;
        }
    }
    private IEnumerator ReduceValue()
    {
        while (CurrentValue > 0)
        {
            CurrentValue -= DecayValue; // Reduce the value
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }
    }
}
