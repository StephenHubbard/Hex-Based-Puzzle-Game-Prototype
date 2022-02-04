using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementPoint : MonoBehaviour
{
    [SerializeField] GameObject placementSphere;

    void Start()
    {
        if (transform.parent.GetComponent<Tile>().isPlaced) {
            GameObject newSphere = Instantiate(placementSphere, transform.position, transform.rotation);
            newSphere.transform.parent = transform.parent;
            Destroy(gameObject);
        }
    }
}
