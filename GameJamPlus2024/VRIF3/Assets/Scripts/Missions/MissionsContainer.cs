using UnityEngine;

[CreateAssetMenu(fileName = "MissionsContainer", menuName = "Scriptable Objects/MissionsContainer")]
public class MissionsContainer : ScriptableObject
{
    public MissionData[] missionsData;
}
