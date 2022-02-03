using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacementPoint : MonoBehaviour
{
    [SerializeField] GameObject placementSphere;
    [SerializeField] Transform placementSpheres;


    void Start()
    {
        GameObject newSphere = Instantiate(placementSphere, transform.position, transform.rotation);
        newSphere.transform.parent = GameObject.Find("Placement Spheres").transform;
    }

    void Update()
    {
    }

    
}
