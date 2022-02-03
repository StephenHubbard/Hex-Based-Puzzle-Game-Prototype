using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isValidPlacement = false;

    public GameObject currentPlacementSphere = null;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlacementSphere>()) {
            currentPlacementSphere = other.gameObject;
            if (currentPlacementSphere.GetComponent<PlacementSphere>().isOccupied == false) {
                isValidPlacement = true;
            } else {
                isValidPlacement = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<PlacementSphere>()) {
            isValidPlacement = false;
        }
    }
}
