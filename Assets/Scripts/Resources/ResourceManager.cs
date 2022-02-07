using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] public int totalForest;
    [SerializeField] public int totalGrain;
    [SerializeField] public int totalBrick;
    [SerializeField] public int totalSheep;

    [SerializeField] private TMP_Text totalForestText;
    [SerializeField] private TMP_Text totalGrainText;
    [SerializeField] private TMP_Text totalBrickText;
    [SerializeField] private TMP_Text totalSheepText;

    [SerializeField] private TMP_Text newTotalForestText;
    [SerializeField] private TMP_Text newTotalGrainText;
    [SerializeField] private TMP_Text newTotalBrickText;
    [SerializeField] private TMP_Text newTotalSheepText;

    [SerializeField] private int forestGainedThisTurn;
    [SerializeField] private int grainGainedThisTurn;
    [SerializeField] private int brickGainedThisTurn;
    [SerializeField] private int sheepGainedThisTurn;

    private void Update() {
        totalForestText.text = totalForest.ToString();
        totalGrainText.text = totalGrain.ToString();
        totalBrickText.text = totalBrick.ToString();
        totalSheepText.text = totalSheep.ToString();

        newTotalForestText.text = "+ " + forestGainedThisTurn.ToString();
        newTotalGrainText.text = "+ " + grainGainedThisTurn.ToString();
        newTotalBrickText.text = "+ " + brickGainedThisTurn.ToString();
        newTotalSheepText.text = "+ " + sheepGainedThisTurn.ToString();
    }

    public void EndTurnResourceCalc() {
        totalForest += forestGainedThisTurn;
        totalGrain += grainGainedThisTurn;
        totalBrick += brickGainedThisTurn;
        totalSheep += sheepGainedThisTurn;
    }

    public void CalcNewResourcesGainedThisTurn() {
        forestGainedThisTurn = 0;
        grainGainedThisTurn = 0;
        brickGainedThisTurn = 0;
        sheepGainedThisTurn = 0;

        Building[] allBuildings = FindObjectsOfType<Building>();
        foreach (Building building in allBuildings)
        {
            if (building.buildingType.buildingName == "Gatherer Hut") {
                List<GameObject> tilesThisBuildingTouches = building.getNearbyTilesList();

                foreach (GameObject tile in tilesThisBuildingTouches)
                {
                    Tile thisTile = tile.GetComponent<Tile>();

                    if (thisTile.tileType.name == "Forest" && thisTile.isReadyForHarvestFn()){
                        forestGainedThisTurn++;
                    }

                    if (thisTile.tileType.name == "Grain" && thisTile.isReadyForHarvestFn()){
                        grainGainedThisTurn++;
                    }

                    if (thisTile.tileType.name == "Brick" && thisTile.isReadyForHarvestFn()){
                        brickGainedThisTurn++;
                    }

                    if (thisTile.tileType.name == "Sheep" && thisTile.isReadyForHarvestFn()){
                        sheepGainedThisTurn++;
                    }
                }
            }

        }
    }
}
