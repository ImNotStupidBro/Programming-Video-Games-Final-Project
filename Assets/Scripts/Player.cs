using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    // STATISTICS
    [SerializeField] private float xSpeed;
    [SerializeField] private float ySpeed;

    [SerializeField] private int healthPoints = 6;
    [SerializeField] private int healthPotions = 0;

    private float xInput;
    public GameObject meleeAttackPosition;
    public Vector2 meleeAttackSize;
    public int damage;
    public LayerMask enemies;

    // META OBJECTS
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D col;

    // PLAYER STATES
    private bool running;
    private bool grounded;
    private bool airborne;
    private bool falling;
    private bool crouching;
    private bool attackingMelee;
    private bool attackingRanged;
    private bool lowHealth;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) {
            DamagePlayer(1);
        }

        if (Input.GetKeyUp(KeyCode.Q)) {
            HealPlayer(1);
        }

        HealthCheck();
        
        MeleeAttackCheck();

        MoveHorizontally();
        
        FallingCheck();

        FlipPlayerHorizontal();

        JumpCheck();

        CrouchCheck();

        AnimatorUpdater();
    }

    // UPDATE FUNCTIONS
    private void JumpCheck()
    {
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }

    private void FallingCheck()
    {
        if(body.velocity.y < 0.0f)
        {
            falling = true;
        } else {
            falling = false;
        }
    }

    private void CrouchCheck()
    {
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && grounded)
        {
            Crouch();
        }

        if ((Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) && grounded)
        {
            Uncrouch();
        }
    }

    private void MeleeAttackCheck()
    {
        if ((Input.GetMouseButtonDown(0)) && grounded)
        {
            body.velocity = new Vector2(0, 0);
            attackingMelee = true;
        }
        
        if ((Input.GetMouseButtonUp(0)) && grounded)
        {
            attackingMelee = false;
        }
    }

    private void HealthCheck()
    {
        if (this.GetHealthPoints() < 2){
            lowHealth = true;
        } else {
            lowHealth = false;
        }
    }

    private void AnimatorUpdater()
    {
        //Updates Animator flags
        anim.SetBool("isRunning", xInput != 0);
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("isAirborne", airborne);
        anim.SetBool("isFalling", falling);
        anim.SetBool("isCrouching", crouching);
        anim.SetFloat("playerYSpeed", body.velocity.y);
        anim.SetBool("isAttackingMelee", attackingMelee);
        anim.SetBool("hasLowHealth", lowHealth);
    }

    private void FlipPlayerHorizontal()
    {
        // Flips player sprite
        if (xInput > 0.01f)
        {
            transform.localScale = new Vector2(2, 2);
        } else if (xInput < -0.01f)
        {
            transform.localScale = new Vector2(-2, 2);
        }
    }
    
    // ACTION FUNCTIONS
    private void MoveHorizontally()
    {
        if(!attackingMelee){
            xInput = Input.GetAxis("Horizontal");
            body.velocity = new Vector2(xInput * xSpeed, body.velocity.y);
        }
    }
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, ySpeed);
        grounded = false;
        airborne = true;
    }
    private void Crouch() 
    {
        crouching = true;
        col.size = new Vector2(0.5f, 0.5f);
        col.offset = new Vector2(0, -0.25f);
    }

    private void Uncrouch()
    {
        crouching = false;
        col.size = new Vector2(0.5f, 1.0f);
        col.offset = new Vector2(0, 0);
    }

    private void MeleeAttack()
    {
        if(attackingMelee)
        {
            Collider2D[] enemy = Physics2D.OverlapBoxAll(meleeAttackPosition.transform.position, meleeAttackSize, enemies);

            foreach(Collider2D enemyGameObject in enemy)
            {
                if(enemyGameObject.tag == "Enemy")
                {
                    enemyGameObject.GetComponent<Enemy>().DamageEnemy(damage);
                    Debug.Log("Hit enemy.");
                }
            }
        }
    }

    // OBJECT INTERACTIONS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            grounded = true;
            airborne = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Platform")
        {
            grounded = false;
            airborne = true;
        }
    }

    // RETURN AND MODIFY FUNCTIONS
    private void SetHealthPoints(int set) { healthPoints = set; }
    private void SetHealthPotions(int set) { healthPotions = set; }

    public int GetHealthPoints() { return healthPoints; }
    public int GetHealthPotions() { return healthPotions; }

    public void HealPlayer(int HP) { healthPoints += HP; }
    public void DamagePlayer(int DMG) { healthPoints -= DMG; }
    public void IncrementPotionCounter(int delta) { healthPoints += delta; }
    public void DecrementPotionCounter(int delta) { healthPoints -= delta; }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(meleeAttackPosition.transform.position, meleeAttackSize);
    }
}
