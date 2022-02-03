using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private float seperationSizeX;
    [SerializeField] private float seperationSizeY;
    [SerializeField] private float seperationSizeZ;
    [SerializeField] private float gridSize;
    [SerializeField] private GameObject sphere;

    private void Start() {
        InstantiateSpheres();
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / seperationSizeX);
        int yCount = Mathf.RoundToInt(position.y / seperationSizeY);
        int zCount = Mathf.RoundToInt(position.z / seperationSizeZ);

        Vector3 result = new Vector3(
            (float)xCount * seperationSizeX,
            (float)yCount * seperationSizeY,
            (float)zCount * seperationSizeZ);

        result += transform.position;

        return result;
    }

    private void InstantiateSpheres()
    {
        for (float x = 0; x < gridSize; x += seperationSizeX)
        {
            for (float z = 0; z < gridSize; z += seperationSizeZ)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                GameObject newSphere = Instantiate(sphere, point, transform.rotation);
            }
        }
    }
}