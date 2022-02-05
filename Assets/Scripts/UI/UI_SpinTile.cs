using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SpinTile : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 20f;
    [SerializeField] public TileType tileType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
    }
}
