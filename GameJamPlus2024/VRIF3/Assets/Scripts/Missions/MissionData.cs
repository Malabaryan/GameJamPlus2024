using System;
using UnityEngine;

[Serializable]
public class MissionData
{
    [SerializeField] private string missionName;
    [SerializeField] private string missionDetail;
    [SerializeField] private string missionExtra;
    [SerializeField] private GameObject missionInterface; //This should be a prefab gameobject with a UI panel containing mission visuals ordered
}
