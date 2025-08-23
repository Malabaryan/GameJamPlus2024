using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    int currentScore = 0;

    public void setScore(int score)
    {
        currentScore = score;
        GameObject.Find("GameManager").GetComponent<ScoreManager>().AddScore(score);
        scoreLabel.text = score.ToString();
    }

}
