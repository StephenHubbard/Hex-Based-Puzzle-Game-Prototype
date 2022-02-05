using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpcomingTiles : MonoBehaviour
{

    [SerializeField] private GameObject[] hexTilesUI;
    [SerializeField] private GameObject activeTile;

    private void Awake() {
        foreach (Transform child in transform)
        {
            if (child.transform.gameObject.GetComponent<UI_SpinTile>()) {
                activeTile = child.gameObject;
            }
        }
    }

    public void getNewTile() {
        int randomTileNum = Random.Range(0, hexTilesUI.Length);
        Destroy(activeTile);
        GameObject newTile = Instantiate(hexTilesUI[randomTileNum], transform.position, hexTilesUI[randomTileNum].transform.rotation);
        newTile.transform.SetParent(transform);
        newTile.transform.localScale = new Vector3(100, 100, 100);
        newTile.transform.rotation = activeTile.transform.rotation;
        activeTile = newTile;

    }
}
