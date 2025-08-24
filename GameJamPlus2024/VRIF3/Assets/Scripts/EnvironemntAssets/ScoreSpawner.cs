using UnityEngine;

public class ScoreSpawner : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private GameObject scoreUIPrefab;

    public void SpawnScore()
    {
        GameObject scorePrefab = Instantiate(scoreUIPrefab, transform.position, Quaternion.identity);
        scorePrefab.GetComponent<ScoreUI>().setScore(score);
        GameManager.Instance.ScoreManager.AddScore(score);
    }
}
