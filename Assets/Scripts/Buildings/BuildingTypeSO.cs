using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    public string buildingName;
    public GameObject buildingPrefab;
    public bool isRoad;
    public int ForestNeededToBuy;
    public int GrainNeededToBuy;
    public int BrickNeededToBuy;
    public int SheepNeededToBuy;
    public int PopulationNeedeToBuy;
}
