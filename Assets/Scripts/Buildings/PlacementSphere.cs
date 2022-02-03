using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSphere : MonoBehaviour
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
        if (other.gameObject.GetComponent<PlacementSphere>()) {
            if (!other.gameObject.GetComponent<PlacementSphere>().isDestroyed) {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }

}
