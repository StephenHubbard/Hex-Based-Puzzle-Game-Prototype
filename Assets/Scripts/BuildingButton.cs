using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class BuildingButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject buildingPrefab;
    
    private GameObject buildingPreviewInstance;

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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    private void UpdateBuildingPreview()
    {
        if (!buildingPreviewInstance) {return;}

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);

        buildingPreviewInstance.transform.position = hit.point;
    
    }
}
