using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private DeliverBoxBehavior deliverBox;
    [SerializeField] private MissionsContainer missionsContainer;

    [SerializeField] private Transform missionCanvas;

    public AudioClip finishedDemo;

    public int currentMission = 0;


    private void Start()
    {
        Instantiate(missionsContainer.missionsData[0].missionInterface, missionCanvas);
        deliverBox.UpdateDesiredFlower(missionsContainer.missionsData[0].desiredFlower);
        //StartCoroutine(PassMission()); Debugging only
    }

    public void SetMission(int mission)
    {
        currentMission = mission;
    }

    public int GetMission()
    {
        return currentMission;
    }

    public void CompleteMission()
    {
        if (currentMission + 1 == missionsContainer.missionsData.Length)
            return;

        if (missionCanvas.GetChild(0) != null)
        {
            Destroy(missionCanvas.GetChild(0).gameObject);
            currentMission += 1;
        }
        Instantiate(missionsContainer.missionsData[currentMission].missionInterface, missionCanvas);
        deliverBox.UpdateDesiredFlower(missionsContainer.missionsData[currentMission].desiredFlower);
    }

    IEnumerator PassMission()
    {
        for (int i = 0; i < missionsContainer.missionsData.Length; i++)
        {
            yield return new WaitForSeconds(3f);
            CompleteMission();
        }
    }
}
