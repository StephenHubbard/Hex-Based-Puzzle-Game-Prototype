using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] public TileType tileType;
    [SerializeField] public GameObject resourceCounterCanvas;
    [SerializeField] private TMP_Text resourceTickCounterText;
    [SerializeField] private int amountOfResourceTickCounter;
    [SerializeField] private Slider slider;
    [SerializeField] public Transform placementRoadParent;

    [SerializeField] public bool isMountain = false;

    public bool isPlaced = false;

    private void Awake() {
        if (isMountain) {return;}

        if (FindObjectOfType<ResourceInputControls>().isToggledOn) {
            resourceCounterCanvas.SetActive(true);
        }
    }

    private void Start()
    {
        RenameTile();
        GenerateResourceTickAmount();
        isReadyForHarvestFn();
    }

    private void Update() {
        if (isMountain) {return;}

        resourceTickCounterText.text = amountOfResourceTickCounter.ToString();
    }

    private void RenameTile()
    {
        if (isPlaced)
        {
        transform.parent = GameObject.Find("Hex Tiles").transform;
        gameObject.name = "tile (" + transform.position.x.ToString() + ", " + transform.position.z.ToString() + ")";
        }
    }

    private void GenerateResourceTickAmount() {
        if (isMountain) {return;}

        int randomNum = Random.Range(1, 6);
        amountOfResourceTickCounter = randomNum;
        slider.value = Random.Range(0, randomNum + 1);
        slider.maxValue = amountOfResourceTickCounter;
    }

    public bool isReadyForHarvestFn() {

        if (isMountain) {
            return false;
        }

        if (amountOfResourceTickCounter == slider.value) {
            return true;
        } else {
            return false;
        }
    }

    public void EndTurnResourceTileCalc() {
        if (isMountain) {return;}


        if (slider.value < amountOfResourceTickCounter) {
            slider.value++;
        } else {
            slider.value = 0;
        }
    }
}
