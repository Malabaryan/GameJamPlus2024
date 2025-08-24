using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLabel;
    int currentScore = 0;

    private void Start()
    {
        transform.LookAt(GameObject.Find("Ship").transform);
        Destroy(gameObject, 3f);
    }

    public void setScore(int score)
    {
        currentScore = score;
        GameManager.Instance.ScoreManager.AddScore(score);
        scoreLabel.text = score.ToString();
    }

    private void Update()
    {
        scoreLabel.transform.Translate(Vector3.up * Time.deltaTime * 2f);
    }

}
