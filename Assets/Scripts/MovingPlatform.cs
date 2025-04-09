using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingPlatform : MonoBehaviour
{
    public float speed;
    public int startingPoint;
    public Transform[] points;

    private int i;

    void Start()
    {
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            if(i == points.Length) { i = 0; }            
        }
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = this.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
