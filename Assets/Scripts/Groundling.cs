using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundling : Enemy
{
    public Groundling()
    {
        healthPoints = 3;
        movementSpeed = (float)1.0;
    }
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        body.velocity = new Vector2(movementSpeed * direction, body.velocity.y);
    }
}
