using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;


public class ResourceInputControls : MonoBehaviour
{
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

    [SerializeField] private Transform hexTiles;

    public bool isToggledOn = false;


    void Start()
    {
        
    }

    void Update()
    {
        TabToggleResourceChart();
        TabToggleResourceTileCanvas();
    }

    private void TabToggleResourceChart() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            totalForestText.gameObject.SetActive(!totalForestText.gameObject.activeInHierarchy);
            totalGrainText.gameObject.SetActive(!totalGrainText.gameObject.activeInHierarchy);
            totalMountainText.gameObject.SetActive(!totalMountainText.gameObject.activeInHierarchy);
            totalBrickText.gameObject.SetActive(!totalBrickText.gameObject.activeInHierarchy);
            totalSheepText.gameObject.SetActive(!totalSheepText.gameObject.activeInHierarchy);

            newTotalForestText.gameObject.SetActive(!newTotalForestText.gameObject.activeInHierarchy);
            newTotalGrainText.gameObject.SetActive(!newTotalGrainText.gameObject.activeInHierarchy);
            newTotalMountainText.gameObject.SetActive(!newTotalMountainText.gameObject.activeInHierarchy);
            newTotalBrickText.gameObject.SetActive(!newTotalBrickText.gameObject.activeInHierarchy);
            newTotalSheepText.gameObject.SetActive(!newTotalSheepText.gameObject.activeInHierarchy);
        }
    }

    private void TabToggleResourceTileCanvas() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            foreach (Transform tile in hexTiles)
            {
                GameObject thisTile = tile.GetComponent<Tile>().resourceCounterCanvas;
                thisTile.SetActive(!thisTile.activeInHierarchy);
            }

            isToggledOn = !isToggledOn;
        } 
    }
}
