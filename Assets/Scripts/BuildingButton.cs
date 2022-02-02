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

    private Camera mainCamera;



    private void Awake() {
        mainCamera = Camera.main;
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (buildingPreviewInstance)
        {
            UpdateBuildingPreview();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) {return;}

        buildingPreviewInstance = Instantiate(buildingPrefab);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (buildingPreviewInstance== null) {return;}

        GameObject newBuilding = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, tileMask))
        {
            if (TestIsValidPlacement()) {
                newBuilding = InstantiateNewBuilding(newBuilding, hit);
            }
        }


        Destroy(buildingPreviewInstance);
    }

    private void UpdateBuildingPreview()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, tileMask))
        {
            return;
        }

        buildingPreviewInstance.transform.position = hit.point;
    }

    private GameObject InstantiateNewBuilding(GameObject newBuilding, RaycastHit hit) {
        newBuilding = Instantiate(buildingPrefab, hit.point, Quaternion.identity);

        newBuilding.transform.rotation = buildingPreviewInstance.transform.rotation;

        buildingPreviewInstance.GetComponent<Building>().currentPlacementSphere.GetComponent<PlacementSphere>().isOccupied = true;
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
