using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TileType")]
public class TileType : ScriptableObject
{
    public string tileName;
    public GameObject tilePrefab;
    public int tileIndexNum;
    public int TreeValue;
    public int SheepValue;
    public int GrainValue;
    public int BrickValue;
    public int MountainValue;
}
