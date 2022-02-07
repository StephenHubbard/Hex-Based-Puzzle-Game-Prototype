using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private ShrineManager shrineManager;
    [SerializeField] private Transform hexTiles;
    [SerializeField] private Transform placementValidHexes;
    [SerializeField] private GameObject mountainPrefab;
    [SerializeField] private int amountOfMountainsToSpawn = 1;

    private int currentTurn = 1;



    public void EndTurnButtonFn() {
        currentTurn++;
        resourceManager.EndTurnResourceCalc();
        IncreaseEachHexTileSliderValue();
        resourceManager.CalcNewResourcesGainedThisTurn();
        shrineManager.EndTurnShrineCounterReduce();
        SpawnMountains();
    }

    private void IncreaseEachHexTileSliderValue() {
        foreach (Transform tile in hexTiles)
        {
            tile.GetComponent<Tile>().EndTurnResourceTileCalc();
        }
    }

    private void SpawnMountains() {
        int amountOfValidHexes = placementValidHexes.transform.childCount;
        int randomNum = Random.Range(0, amountOfValidHexes);
        print(randomNum);
    }
}
