using System;
using UnityEngine;

[Serializable]
public class FlowerData
{
    public SeedBehavior.SeedType SeedType;
    public GameObject seedPrefab;
    public GameObject flowerPrefab; //This might not be needed but I'll try to avoid too deep references
}
