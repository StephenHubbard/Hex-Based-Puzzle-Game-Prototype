using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;



public class PlaceTileButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject[] hexTiles;
    [SerializeField] private LayerMask validTilePlacementMask = new LayerMask();
    [SerializeField] private Transform PlacementValidHexesTransformParent;
    [SerializeField] private int sheepCost = 1;
    [SerializeField] private int brickCost = 1;
    [SerializeField] private GameObject costImageContainer;

    

    private GameObject tilePrefab;
    private GameObject tilePreviewInstance;
    
    private UpcomingTiles upcomingTiles;
    private Camera mainCamera;

    private ResourceManager resourceManager;

    private void Awake() {
        mainCamera = Camera.main;
        upcomingTiles = FindObjectOfType<UpcomingTiles>();
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    void Start()
    {
    }

    void Update()
    {
        UpdateTilePreviewInstance();
        RotateTile();
    }

    public void OnHoverOver() {
        costImageContainer.SetActive(true);
    }

    public void OnHoverExit() {
        costImageContainer.SetActive(false);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) {return;}

        if (CanAffordTile()) {
            int randomTileNum = Random.Range(0, hexTiles.Length);
            Vector3 spawnPoint = new Vector3(0, 0, -10);
            tilePreviewInstance = Instantiate(FindObjectOfType<UI_SpinTile>().tileType.tilePrefab, spawnPoint, FindObjectOfType<UI_SpinTile>().tileType.tilePrefab.transform.rotation);
            
            if (tilePreviewInstance.GetComponent<Tile>().resourceCounterCanvas) {
                tilePreviewInstance.GetComponent<Tile>().resourceCounterCanvas.SetActive(false);
            }
            tilePrefab = tilePreviewInstance;
            tilePreviewInstance.layer = 0;
            tilePreviewInstance.GetComponent<MeshRenderer>().enabled = false;
            
            foreach (Transform child in PlacementValidHexesTransformParent)
            {
                child.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
        }

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (tilePreviewInstance == null) {return;}

        GameObject newTile = null;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, validTilePlacementMask))
        {
            if (TestIsValidPlacement()) {
                newTile = InstantiateNewTile(newTile, hit);
                upcomingTiles.getNewTile();
            }
        }

        foreach (Transform child in PlacementValidHexesTransformParent)
        {
            child.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        Destroy(tilePreviewInstance);

    }

    private void UpdateTilePreviewInstance()
    {
        if (!tilePreviewInstance) {return;}

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, validTilePlacementMask))
        {
            return;
        }

        tilePreviewInstance.GetComponent<MeshRenderer>().enabled = true;

        tilePreviewInstance.transform.position = hit.transform.gameObject.transform.position;
    }

    private GameObject InstantiateNewTile(GameObject newTile, RaycastHit hit) {
        newTile = Instantiate(tilePrefab, hit.point, Quaternion.identity);
        newTile.GetComponent<Tile>().isPlaced = true;
        newTile.transform.rotation = tilePreviewInstance.transform.rotation;
        newTile.layer = LayerMask.NameToLayer("Tile");
        CenterNewTile(newTile, hit);
        newTile.GetComponent<MeshCollider>().enabled = true;
        BuyTile();
        return newTile;
    }

    private bool TestIsValidPlacement() {
        return true;
    }

    private void RotateTile() {
        if (!tilePreviewInstance) {return;}

        if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1)) {
            tilePreviewInstance.transform.Rotate(0, 0, 60);
        }
    }

    private void CenterNewTile(GameObject newTile, RaycastHit hit) {
        // hacky way to force perfect positioning instead of correct math in previous function(s);
        
        newTile.transform.position = hit.collider.gameObject.transform.position;

        var xValue = Mathf.Round(newTile.transform.position.x);
        var zValue = Mathf.Round(newTile.transform.position.z / 1.7f) * 1.7f;

        newTile.transform.position = new Vector3(xValue, 0, zValue);
    }

    private bool CanAffordTile() {
        if (resourceManager.totalSheep >= sheepCost && resourceManager.totalBrick >= brickCost) {
            return true;
        } else {
            print("can NOT afford tile");
            return false;
        }
    }

    private void BuyTile() {
        resourceManager.totalSheep -= sheepCost;
        resourceManager.totalBrick -= brickCost;
    }

}
