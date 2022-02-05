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

    public bool isPlaced = false;

    private void Awake() {
        if (FindObjectOfType<ResourceInputControls>().isToggledOn) {
            resourceCounterCanvas.SetActive(true);
        }
    }

    private void Start()
    {
        RenameTile();
        GenerateResourceTickAmount();
    }

    private void Update() {
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
        int randomNum = Random.Range(1, 6);
        amountOfResourceTickCounter = randomNum;
        slider.value = Random.Range(0, randomNum + 1);
        slider.maxValue = amountOfResourceTickCounter;
    }
}
