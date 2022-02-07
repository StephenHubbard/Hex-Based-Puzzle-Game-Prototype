using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public bool isPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (isPlaced) {
            if (other.gameObject.GetComponent<PlacementSphere>()) {
                other.gameObject.GetComponent<PlacementSphere>().isNearRoad = true;
            }
        }
    }
}
