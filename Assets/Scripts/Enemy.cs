using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // STATISTICS
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected int healthPoints;
    [SerializeField] protected int direction;

    // META OBJECTS
    protected Rigidbody2D body;
    protected Animator anim;
    protected BoxCollider2D col;

    // ENEMY STATES
    protected bool grounded;
    protected bool airborne;
    protected bool attacking;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        direction = -1;
    }

    public void Dead()
    {
        // Death particle effect
        Destroy(gameObject);
    }

    public int GetHealthPoints() { return healthPoints; }
    protected void SetHealthPoints(int set) { healthPoints = set; }
    public void HealEnemy(int HP) { healthPoints += HP; }
    public void DamageEnemy(int DMG) 
    { 
        healthPoints -= DMG;
        if (healthPoints <= 0) { 
            Invoke("Dead", 0.0f); 
        }
    }
}
