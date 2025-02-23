using UnityEngine;
using CI.QuickSave;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;


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
        List<int> listPotFlower = reader.Read<List<int>> ("PotsFlower");
        List<bool> listFlowerState = reader.Read<List<bool>>("PotsFlowerState");
        int index = 0;
        foreach (var pot in GameManager.Instance.MissionManager.PotList)
        {
            if(listPotFlower[index] != -1)
            {
                var flowerData = GameManager.Instance.MissionManager.FlowersContainer.flowersData[listPotFlower[index]];
                
                pot.SeedType = flowerData.SeedType;
                pot.flowerPrefab = flowerData.flowerPrefab;
                pot.hasBeenPlanted = true;
                pot.turnipIndicator.SetActive(!listFlowerState[index]);
                if(listFlowerState[index])
                    StartCoroutine(pot.SpawnGrabbableFlower());


            }
            index++;
        }
    }

    // Update is called once per frame
    public void Save()
    {
        var missionNumber = GameManager.Instance.MissionManager.GetMission();
        var writer = QuickSaveWriter.Create("Player");
        writer.Write("Mission", missionNumber);
        writer.Write("Position", GameManager.Instance.ShipGameObject.transform.position);

        List<int> listPotFlower = new();
        List<bool> listFlowerState = new();
        foreach (var pot in GameManager.Instance.MissionManager.PotList)
        {
            int index = 0;
            if (pot.SeedType == SeedBehavior.SeedType.None)
            {
                index = -1;
            }
            else
            {
                while (index < GameManager.Instance.MissionManager.FlowersContainer.flowersData.Length)
                {
                    if (GameManager.Instance.MissionManager.FlowersContainer.flowersData[index].SeedType == pot.SeedType)
                    {
                        break;
                    }
                    index++;
                }
            }
            listPotFlower.Add(index);
            listFlowerState.Add(!pot.turnipIndicator.activeSelf);
        }
        writer.Write("PotsFlower", listPotFlower);
        writer.Write("PotsFlowerState", listFlowerState);

        writer.Commit();
    }


}

