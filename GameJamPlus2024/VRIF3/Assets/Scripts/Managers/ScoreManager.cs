using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int bestScore = 0;

    private bool isTrailTimerRunning = false;
    [SerializeField] private int currentScore = 0;

    [SerializeField] private TextMeshProUGUI scoreLabel;

    public void AddScore(int score)
    {
        if (isTrailTimerRunning)
            currentScore += score;

        scoreLabel.text = currentScore.ToString();
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
