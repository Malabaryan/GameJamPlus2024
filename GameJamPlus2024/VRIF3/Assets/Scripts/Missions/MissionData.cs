using System;
using UnityEngine;

[Serializable]
public class MissionData
{
    public string missionName;
    public string missionDetail;
    public string missionExtra;
    public GameObject missionInterface; //This should be a prefab gameobject with a UI panel containing mission visuals ordered
    public SeedBehavior.SeedType desiredFlower;
}
