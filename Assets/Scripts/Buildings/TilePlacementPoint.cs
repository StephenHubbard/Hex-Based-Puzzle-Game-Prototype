using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePlacementPoint : MonoBehaviour
{
    [SerializeField] GameObject placementHex;

    void Start()
    {
        if (transform.parent.GetComponent<Tile>().isPlaced) {
            GameObject newSphere = Instantiate(placementHex, transform.position, transform.rotation);
            newSphere.transform.parent = GameObject.Find("Placement Valid Hexes").transform;
            Destroy(gameObject);
        }
    }

}
