using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public SaveManager SaveManager;
    public MissionManager MissionManager;
    public ScoreManager ScoreManager;
    public TrailManager TrailManager;
    public GameObject ShipGameObject;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SaveManager.Load();
    }

    public void SetTrail(bool state)
    {
        ScoreManager.SetTrailState(state);
        if(state)
            TrailManager.StartTimer();
    }
}
