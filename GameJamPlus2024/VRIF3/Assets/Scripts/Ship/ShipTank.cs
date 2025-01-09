using UnityEngine;

public class ShipTank : MonoBehaviour
{
    public ProgressBar ProgressBar;
  

    public void SetGasAmount(float gasAmount)
    {
        ProgressBar.BarValue = gasAmount;

    }
}
