using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LayerMask tileMask = new LayerMask();
    [SerializeField] private GameObject buildingPreviewInstance;
    [SerializeField] private Transform hexTilesParent;
    [SerializeField] private BuildingTypeSO buildingType;

    private Camera mainCamera;
    private ResourceManager resourceManager;


    private void Awake() {
        mainCamera = Camera.main;
        resourceManager = FindObjectOfType<ResourceManager>();
    }


    void Update()
    {
        UpdateBuildingPreview();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) {return;}

        if (CanAffordBuilding()) {
            buildingPreviewInstance = Instantiate(buildingType.buildingPrefab);
            buildingPreviewInstance.GetComponent<MeshRenderer>().enabled = false;
            if (buildingType.buildingName == "Road") {
                TogglePlacementRoadsVisible();
            } else {
                TogglePlacementSpheresVisible();
            }
        } 

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreviewInstance== null) {return;}

        GameObject newBuilding = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere || buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad)
        {
            if (TestIsValidPlacement()) {
                newBuilding = InstantiateNewBuilding(newBuilding);
            }
        }


        Destroy(buildingPreviewInstance);

        TogglePlacementSpheresHidden();
        TogglePlacementRoadsHidden();
    }

    private void UpdateBuildingPreview()
    {
        if (!buildingPreviewInstance) {return;}

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, tileMask))
        {
            return;
        }

        buildingPreviewInstance.transform.position = hit.point;

        buildingPreviewInstance.GetComponent<MeshRenderer>().enabled = true;

        if (buildingType.buildingName == "Road" && buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad) {
            buildingPreviewInstance.transform.rotation = buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad.transform.rotation;
        }

    }

    private void TogglePlacementSpheresVisible() {
            foreach (Transform child in hexTilesParent)
            {
                foreach (Transform sphere in child)
                {
                    if (sphere.gameObject.GetComponent<PlacementSphere>()) {
                        if (sphere.gameObject.GetComponent<PlacementSphere>().isNearRoad) {
                            sphere.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                }
            }
        }

    private void TogglePlacementSpheresHidden() {
        foreach (Transform child in hexTilesParent)
            {
                foreach (Transform sphere in child)
                {
                    // for world space canvas ui prefab
                    if (sphere.GetComponent<MeshRenderer>()) {
                        sphere.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
    }

    private void TogglePlacementRoadsVisible() {
        foreach (Transform hexTile in hexTilesParent)
        {
            Transform thisTilesRoadParent = hexTile.GetComponent<Tile>().placementRoadParent;

            foreach (Transform roadInstance in thisTilesRoadParent)
            {
                roadInstance.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    private void TogglePlacementRoadsHidden() {
        foreach (Transform hexTile in hexTilesParent)
        {
            Transform thisTilesRoadParent = hexTile.GetComponent<Tile>().placementRoadParent;

            foreach (Transform roadInstance in thisTilesRoadParent)
            {
                roadInstance.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    private GameObject InstantiateNewBuilding(GameObject newBuilding) {
        if (buildingType.buildingName == "Road") {
            newBuilding = Instantiate(buildingType.buildingPrefab, buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad.transform.position, Quaternion.identity);
            newBuilding.transform.rotation = buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad.transform.rotation;
            buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad.GetComponent<PlacementRoad>().isOccupied = true;
            Destroy(buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad.GetComponent<PlacementRoad>().gameObject);
            Transform placementRoadTransform = buildingPreviewInstance.GetComponent<Building>().currentPlacementRoad.transform;
            newBuilding.transform.position = placementRoadTransform.position;
            newBuilding.GetComponent<Building>().whatKindOfBuilding();
            newBuilding.GetComponent<Road>().isPlaced = true;
            BuyBuilding();
            return newBuilding;
        } else {
            newBuilding = Instantiate(buildingType.buildingPrefab, buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.transform.position, Quaternion.identity);
            newBuilding.transform.rotation = buildingPreviewInstance.transform.rotation;
            buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.GetComponent<PlacementSphere>().isOccupied = true;
            Destroy(buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.GetComponent<PlacementSphere>().gameObject);
            Transform placementSphereTransform = buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.transform;
            newBuilding.transform.position = placementSphereTransform.position;
            newBuilding.GetComponent<Building>().whatKindOfBuilding();
            BuyBuilding();
            return newBuilding;
        }

    }

    private bool TestIsValidPlacement() {
        if (buildingPreviewInstance.GetComponent<Building>().isValidPlacement) {
            return true;
        } else {
            return false;
        }
    }

    private bool CanAffordBuilding() {
        if (resourceManager.totalForest >= buildingType.ForestNeededToBuy &&
            resourceManager.totalGrain >= buildingType.GrainNeededToBuy &&
            resourceManager.totalSheep >= buildingType.SheepNeededToBuy &&
            resourceManager.totalBrick >= buildingType.BrickNeededToBuy) {
                return true;
            } else {
                print("can NOT afford building");
                return false;
            }
    }

    private void BuyBuilding() {
        resourceManager.totalForest -= buildingType.ForestNeededToBuy;
        resourceManager.totalGrain -= buildingType.GrainNeededToBuy;
        resourceManager.totalSheep -= buildingType.SheepNeededToBuy;
        resourceManager.totalBrick -= buildingType.BrickNeededToBuy;
    }
}
