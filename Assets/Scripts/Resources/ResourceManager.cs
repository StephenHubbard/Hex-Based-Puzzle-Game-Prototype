using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int totalForest;
    [SerializeField] private int totalGrain;
    [SerializeField] private int totalMountain;
    [SerializeField] private int totalBrick;
    [SerializeField] private int totalSheep;

    [SerializeField] private TMP_Text totalForestText;
    [SerializeField] private TMP_Text totalGrainText;
    [SerializeField] private TMP_Text totalMountainText;
    [SerializeField] private TMP_Text totalBrickText;
    [SerializeField] private TMP_Text totalSheepText;

    [SerializeField] private TMP_Text newTotalForestText;
    [SerializeField] private TMP_Text newTotalGrainText;
    [SerializeField] private TMP_Text newTotalMountainText;
    [SerializeField] private TMP_Text newTotalBrickText;
    [SerializeField] private TMP_Text newTotalSheepText;

    [SerializeField] private int forestGainedThisTurn;
    [SerializeField] private int grainGainedThisTurn;
    [SerializeField] private int mountainGainedThisTurn;
    [SerializeField] private int brickGainedThisTurn;
    [SerializeField] private int sheepGainedThisTurn;

    private void Update() {
        totalForestText.text = totalForest.ToString();
        totalGrainText.text = totalGrain.ToString();
        totalMountainText.text = totalMountain.ToString();
        totalBrickText.text = totalBrick.ToString();
        totalSheepText.text = totalSheep.ToString();

        newTotalForestText.text = "+ " + forestGainedThisTurn.ToString();
        newTotalGrainText.text = "+ " + grainGainedThisTurn.ToString();
        newTotalMountainText.text = "+ " + mountainGainedThisTurn.ToString();
        newTotalBrickText.text = "+ " + brickGainedThisTurn.ToString();
        newTotalSheepText.text = "+ " + sheepGainedThisTurn.ToString();
    }

    public void EndTurnResourceCalc() {
        totalForest = 0;
        totalGrain = 0;
        totalMountain = 0;
        totalBrick = 0;
        totalSheep = 0;

        totalForest += forestGainedThisTurn;
        totalGrain += grainGainedThisTurn;
        totalMountain += mountainGainedThisTurn;
        totalBrick += brickGainedThisTurn;
        totalSheep += sheepGainedThisTurn;
    }

    public void CalcNewResourcesGainedThisTurn() {
        forestGainedThisTurn = 0;
        grainGainedThisTurn = 0;
        mountainGainedThisTurn = 0;
        brickGainedThisTurn = 0;
        sheepGainedThisTurn = 0;

        Building[] allBuildings = FindObjectsOfType<Building>();
        foreach (Building building in allBuildings)
        {
            List<GameObject> tilesThisBuildingTouches = building.getNearbyTilesList();

            foreach (GameObject tile in tilesThisBuildingTouches)
            {
                if (tile.GetComponent<Tile>().tileType.name == "Forest"){
                    forestGainedThisTurn++;
                }

                if (tile.GetComponent<Tile>().tileType.name == "Mountain"){
                    mountainGainedThisTurn++;
                }

                if (tile.GetComponent<Tile>().tileType.name == "Grain"){
                    grainGainedThisTurn++;
                }

                if (tile.GetComponent<Tile>().tileType.name == "Brick"){
                    brickGainedThisTurn++;
                }

                if (tile.GetComponent<Tile>().tileType.name == "Sheep"){
                    sheepGainedThisTurn++;
                }
            }
        }
    }
}
