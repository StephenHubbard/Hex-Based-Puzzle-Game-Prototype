using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isPlaced = false;

    private void Start() {
        if (isPlaced) {
            transform.parent = GameObject.Find("Hex Tiles").transform;
            gameObject.name = "tile (" + transform.position.x.ToString() + ", " + transform.position.x.ToString() + ")";
        }
    }
}
