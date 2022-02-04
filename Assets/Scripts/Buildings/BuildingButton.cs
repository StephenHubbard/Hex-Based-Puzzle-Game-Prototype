using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private LayerMask tileMask = new LayerMask();
    [SerializeField] private GameObject buildingPreviewInstance;
    [SerializeField] private Transform hexTilesParent;

    private Camera mainCamera;



    private void Awake() {
        mainCamera = Camera.main;
    }


    void Start()
    {
        
    }

    void Update()
    {
        UpdateBuildingPreview();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) {return;}

        buildingPreviewInstance = Instantiate(buildingPrefab);
        buildingPreviewInstance.GetComponent<MeshRenderer>().enabled = false;
        TogglePlacementSpheresVisible();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreviewInstance== null) {return;}

        GameObject newBuilding = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere)
        {
            if (TestIsValidPlacement()) {
                newBuilding = InstantiateNewBuilding(newBuilding);
            }
        }
        Destroy(buildingPreviewInstance);

        TogglePlacementSpheresHidden();
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
    }

    private void TogglePlacementSpheresVisible() {
            foreach (Transform child in hexTilesParent)
            {
                foreach (Transform sphere in child)
                {
                    sphere.GetComponent<MeshRenderer>().enabled = true;
                }
            }
        }

    private void TogglePlacementSpheresHidden() {
        foreach (Transform child in hexTilesParent)
            {
                foreach (Transform sphere in child)
                {
                    sphere.GetComponent<MeshRenderer>().enabled = false;
                }
            }
    }

    private GameObject InstantiateNewBuilding(GameObject newBuilding) {
        newBuilding = Instantiate(buildingPrefab, buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.transform.position, Quaternion.identity);

        newBuilding.transform.rotation = buildingPreviewInstance.transform.rotation;

        buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.GetComponent<PlacementSphere>().isOccupied = true;
        Destroy(buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.GetComponent<PlacementSphere>().gameObject);
        Transform placementSphereTransform = buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.transform;
        newBuilding.transform.position = placementSphereTransform.position;
        return newBuilding;
    }

    private bool TestIsValidPlacement() {
        if (buildingPreviewInstance.GetComponent<Building>().isValidPlacement) {
            return true;
        } else {
            return false;
        }
    }
}
