using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private ShrineManager shrineManager;
    [SerializeField] private Transform hexTiles;

    private int currentTurn = 1;



    public void EndTurnButtonFn() {
        currentTurn++;
        resourceManager.EndTurnResourceCalc();
        IncreaseEachHexTileSliderValue();
        resourceManager.CalcNewResourcesGainedThisTurn();
        shrineManager.EndTurnShrineCounterReduce();
    }

    private void IncreaseEachHexTileSliderValue() {
        foreach (Transform tile in hexTiles)
        {
            tile.GetComponent<Tile>().EndTurnResourceTileCalc();
        }
    }
}
