using UnityEngine;
using CI.QuickSave;


public class SaveManager : MonoBehaviour
{


    private float autoSaveTimer = 0f;
    private float autoSaveInterval = 300f; // 5 minutes

    void Update()
    {
        autoSaveTimer += Time.deltaTime;

        if (autoSaveTimer >= autoSaveInterval)
        {
            Save();
            autoSaveTimer = 0f;
        }
        if (Input.GetKey(KeyCode.F5))
        {
            Save();
            autoSaveTimer = 0f;
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Load()
    {
        if (!QuickSaveReader.RootExists("Player")) return;
        var reader = QuickSaveReader.Create("Player");
        GameManager.Instance.MissionManager.SetMission(reader.Read<int>("Mission"));
        GameManager.Instance.ShipGameObject.transform.position = reader.Read<Vector3>("Position");
    }

    // Update is called once per frame
    public void Save()
    {
        var missionNumber = GameManager.Instance.MissionManager.GetMission();
        var writer = QuickSaveWriter.Create("Player");
        writer.Write("Mission", missionNumber);
        writer.Write("Position", GameManager.Instance.ShipGameObject.transform.position);
        writer.Commit();
    }


}

