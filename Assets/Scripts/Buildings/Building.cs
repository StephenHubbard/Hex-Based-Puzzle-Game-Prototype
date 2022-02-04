using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isValidPlacement = false;
    public GameObject currentPlacementSphere = null;

    [SerializeField] private List<GameObject> nearbyTiles = new List<GameObject>();
    [SerializeField] private SphereCollider sphereCollider;

    ResourceManager resourceManager;

    private void Awake() {
        resourceManager = FindObjectOfType<ResourceManager>();
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

        if (other.gameObject.GetComponent<Tile>()) {
            nearbyTiles.Add(other.gameObject);
        }

        CalcNewResources();
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<Tile>()) {
            nearbyTiles.Remove(other.gameObject);
        }
    }


    public void CalcNewResources() {
        resourceManager.CalcNewResourcesGainedThisTurn();
    }

    public List<GameObject> getNearbyTilesList() {
        return nearbyTiles;
    }
}
