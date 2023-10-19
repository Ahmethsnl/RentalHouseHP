using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "", fileName = "")]

public class HouseData : ScriptableObject
{
    public List<HouseLevelData> levels;
    public int houselevel;
}
[Serializable]
public class HouseLevelData 
{
    public GameObject housePrefab;
    public int income;
    public int mergePrice;
    public int level;
}
