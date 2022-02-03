using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGen : MonoBehaviour
{
    [SerializeField] int mapWidth = 10;
    [SerializeField] int mapHeight = 10;
    [SerializeField] GameObject[] hexTilePrefabs;
    [SerializeField] float tileXOffset = 1f;
    [SerializeField] float tileZOffset = 1f;
    [SerializeField] Transform hexTiles;

    private void Start() {
        CreateHexTileMap();
    }    

    private void CreateHexTileMap() {
        for (int x = 0; x <= mapWidth; x++) {
            for (int z = 0; z <= mapHeight; z++)
            {
                int randomTileNum = Random.Range(0, hexTilePrefabs.Length);
                GameObject TempGO = Instantiate(hexTilePrefabs[randomTileNum]);

                if(z % 2 == 0) {
                    TempGO.transform.position = new Vector3(x * tileXOffset, 0, z * tileZOffset);
                } else {
                    TempGO.transform.position = new Vector3(x * tileXOffset + tileXOffset / 2, 0, z * tileZOffset);
                }
                SetTileInfo(TempGO, x, z);
            }
        }
    }

    private void SetTileInfo(GameObject GO, int x, int z) {
        GO.transform.parent = hexTiles;
        GO.name = "tile (" + x.ToString() + ", " + z.ToString() + ")";
    }
}
