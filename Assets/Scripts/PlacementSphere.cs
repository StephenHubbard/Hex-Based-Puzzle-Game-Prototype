using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSphere : MonoBehaviour
{
    public bool isOccupied = false;

    private bool isDestroyed;

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
            if (!other.gameObject.GetComponent<PlacementSphere>().isDestroyed) {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }

}
