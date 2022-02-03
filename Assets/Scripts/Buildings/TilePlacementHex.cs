using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacementHex : MonoBehaviour
{
    public bool isOccupied = false;

    private bool isDestroyed;


    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (isOccupied) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<TilePlacementHex>()) {
            if (!other.gameObject.GetComponent<TilePlacementHex>().isDestroyed) {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Tile")) {
            Destroy(gameObject);
        }
    }
}
