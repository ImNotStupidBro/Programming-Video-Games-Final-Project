using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private float currentX;
    private float currentY;

    void Update()
    {
        if(target == null) { return; }
        else 
        {
            if (target.position.x <= minX){ currentX = minX; }
            else if (target.position.x >= maxX) { currentX = maxX; }
            else { currentX = target.transform.position.x; }

            if (target.position.y <= minY){ currentY = minY; }
            else if (target.position.y >= maxY) { currentY = maxY; }
            else { currentY = target.transform.position.y; }
        }
        transform.position = new Vector3 (currentX, currentY, -10);
    }
}
