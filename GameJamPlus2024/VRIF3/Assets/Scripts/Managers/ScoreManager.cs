using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int bestScore = 0;

    private bool isTrailTimerRunning = false;
    private int currentScore = 0;

    public void AddScore(int score)
    {
        if (isTrailTimerRunning)
            currentScore += score;
    }

    public void SetTrailState(bool state)
    {
        isTrailTimerRunning = state;
    }

    public void ResetTrail()
    {
        if (isTrailTimerRunning) currentScore = 0;
    }
}
