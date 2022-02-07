using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSphere : MonoBehaviour
{
    public bool isOccupied = false;
    public bool isNearRoad = false;

    private bool isDestroyed;



    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlacementSphere>()) {
            if (!other.gameObject.GetComponent<PlacementSphere>().isDestroyed) {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }

}
