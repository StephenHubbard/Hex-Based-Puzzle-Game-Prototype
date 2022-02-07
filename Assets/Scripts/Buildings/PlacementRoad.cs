using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementRoad : MonoBehaviour
{
    public bool isOccupied = false;

    private bool isDestroyed;


    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlacementRoad>()) {
            if (!other.gameObject.GetComponent<PlacementRoad>().isDestroyed) {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }
}
