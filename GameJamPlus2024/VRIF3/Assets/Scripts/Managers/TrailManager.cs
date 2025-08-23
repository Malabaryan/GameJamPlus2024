using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TrailManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private bool isTrailRunning = false;

    private void Update()
    {
        if(isTrailRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime < 0)
            {
                remainingTime = 0;
                GameManager.Instance.SetTrail(false);
                isTrailRunning = false;
                //Debug.Log("FINISHED");
                timerText.text = "00:00";
                return;
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void StartTimer()
    {
        isTrailRunning = true;
    }
}
