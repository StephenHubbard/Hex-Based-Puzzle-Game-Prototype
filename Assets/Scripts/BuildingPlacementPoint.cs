using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementPoint : MonoBehaviour
{
    [SerializeField] GameObject placementSphere;


    void Start()
    {
        Instantiate(placementSphere, transform.position, transform.rotation);
    }

    void Update()
    {
        
    }

    
}
