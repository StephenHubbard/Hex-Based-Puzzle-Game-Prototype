using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isValidPlacement = false;
    public bool isBuildingPlaced = false;
    public GameObject currentPlacementSphere = null;
    public GameObject currentPlacementRoad = null;

    [SerializeField] private List<GameObject> nearbyTiles = new List<GameObject>();
    [SerializeField] public BuildingTypeSO buildingType;

    ResourceManager resourceManager;

    private void Awake() {
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlacementSphere>()) {
            currentPlacementSphere = other.gameObject;
            if (currentPlacementSphere.GetComponent<PlacementSphere>().isOccupied == false && currentPlacementSphere.GetComponent<PlacementSphere>().isNearRoad) {
                isValidPlacement = true;
            } else {
                isValidPlacement = false;
            }
        }


        if (other.gameObject.GetComponent<Tile>() && !other.gameObject.GetComponent<Tile>().isMountain) {
            nearbyTiles.Add(other.gameObject);
        } else if (isBuildingPlaced && !buildingType.isRoad && other.gameObject.GetComponent<Road>() && !other.gameObject.GetComponent<Road>().isPlaced) {
            other.gameObject.GetComponent<Road>().isValidRoadPlacement = true;
        }

        CalcNewResources();
    }

    public void DesignateSelectedRoad(GameObject hitRoadFromRaycast) {
        currentPlacementRoad = hitRoadFromRaycast;

        if (currentPlacementRoad.GetComponent<PlacementRoad>().isOccupied == false && currentPlacementRoad.GetComponent<Road>().isValidRoadPlacement) {
                isValidPlacement = true;
            } else {
                isValidPlacement = false;
            }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.GetComponent<Tile>()) {
            nearbyTiles.Remove(other.gameObject);
        } 
    }


    public void CalcNewResources() {
        if (buildingType.name == "Gatherer Hut") {
            resourceManager.CalcNewResourcesGainedThisTurn();
        }
    }

    public List<GameObject> getNearbyTilesList() {
        return nearbyTiles;
    }

    public void whatKindOfBuilding() {
        if (buildingType.name == "House") {
            FindObjectOfType<PopulationManager>().IncreasePopulation();
        }

        if (buildingType.name == "Shrine") {
            FindObjectOfType<ShrineManager>().InceaseCurrentShrineCount();
        }

        if (buildingType.name == "Wizard Tower") {
            FindObjectOfType<ShrineManager>().ResetShrineTimer();
        }
    }
}
