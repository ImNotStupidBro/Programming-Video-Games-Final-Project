using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public float positionX;
    public float positionY;
    public float camMinX;
    public float camMaxX;
    public float camMinY;
    public float camMaxY;
    public GameObject playerSpawnPoint;

    void Awake()
    {
        positionX = gameObject.transform.position.x;
        positionY = gameObject.transform.position.y;
    }
}
