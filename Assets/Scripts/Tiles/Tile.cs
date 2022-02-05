using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public TileType tileType;

    public bool isPlaced = false;

    private void Awake() {
        // print(transform.position.x);
    }

    private void Start() {
        if (isPlaced) {
            transform.parent = GameObject.Find("Hex Tiles").transform;
            gameObject.name = "tile (" + transform.position.x.ToString() + ", " + transform.position.z.ToString() + ")";
        }
    }
}
