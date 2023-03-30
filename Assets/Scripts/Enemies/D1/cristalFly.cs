using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristalFly : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D flyRB;
    public LayerMask groundLayer;
    private GameObject player;
    private Player playerScript;
    private enemy fly;
    private Animator animator;
    public GameObject laserStart;
    public audioManager audioManager;

    [Header("Stats")]
    public float flySpeed;
    public int flyDamage;
    private Vector2 startPos = new Vector2(0, 0);
    private Vector2 flyDestination = new Vector2(0, 0);
    private Vector2 attackPosition = new Vector2(0, 0);
    private bool attack;
    private bool isFlying;
    private bool isAttacking;

    [Header("Laser")]
    public crystalFlyLaser laser;

    [Header("Particles")]
    public ParticleSystem particulaLaser1;

    void Start()
    { 

        startPos = transform.position;
        fly = GetComponent<enemy>();
        flyRB = GetComponent<Rigidbody2D>();    
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<CircleCollider2D>(), true);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(fly.isDead)
        {
            Invoke("DestroyEnemy", 2f);
            //flyRB.velocity = new Vector2(0, 0);
            laser.enabled = false;
            flyRB.gravityScale = 2;
            flyRB.mass = 1;
        }

        if(!fly.isDead)
        {

        }

        if (playerScript.facingDirection > 0) flyDestination = new Vector2(playerScript.transform.position.x + 2.5f, playerScript.transform.position.y -.2f);
        if (playerScript.facingDirection < 0) flyDestination = new Vector2(playerScript.transform.position.x - 2.5f, playerScript.transform.position.y -.2f);

        attackPosition = new Vector2(flyDestination.x, flyDestination.y + 4);

        //if (attack) InvokeRepeating("dealDamage", 2f, 2f);
        //else CancelInvoke("dealDamage");
    }

    void FixedUpdate()
    {
        flyAnimation();

        if (transform.position.x > playerScript.transform.position.x && !fly.isDead && !isAttacking) transform.eulerAngles = new Vector2(0f, 0);
        else if (!fly.isDead && !isAttacking) transform.eulerAngles = new Vector2(0f, 180f);

        if (startPos.x == transform.position.x && startPos.y == transform.position.y) transform.eulerAngles = new Vector2(180f, 0);

        if (!fly.isDead)
        {
            if (Vector3.Distance(transform.position, playerScript.transform.position) < 7f)
            {
                if (attack == true)
                {
                    laser.laserCol1.enabled = true;
                    attackPlayer();
                    StartCoroutine(attackCD());
                }
                else
                {
                    followPlayer();
                }
            }
            else returnToStartPos();
        }
    }

    void flyAnimation()
    {
        if (isFlying)
        {
            animator.SetBool("isFlying", true);
        }
        else
        {
            animator.SetBool("isFlying", false);
        }

        if (transform.position.y == startPos.y && transform.position.x == startPos.x) isFlying = false;

        if(isAttacking)
        {
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }

        if(fly.isDead)animator.SetBool("isDead", true);
    }

    IEnumerator attackCD()
    {
        yield return new WaitForSeconds(1.5f);

        attack = false;
        laser.laserCol1.enabled = false;
    }

    void attackPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, attackPosition, flySpeed * Time.deltaTime);
        if(laser.isHittingPlayer)
        {
            playerScript.playerTakeDamageAUX(false, flyDamage);
        }
        //isFlying = false;
        isAttacking = true;
    }

    void followPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, flyDestination, flySpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, playerScript.transform.position) <= 4 && Mathf.Round(transform.position.y) == Mathf.Round(flyDestination.y)) attack = true;
        isFlying = true;
        isAttacking = false;
    }

    void returnToStartPos()
    {
        if (transform.position.x > startPos.x) transform.eulerAngles = new Vector2(0f, 0);
        transform.position = Vector2.MoveTowards(transform.position, startPos, flySpeed * Time.deltaTime);
        //isFlying = true;
        isAttacking = false;
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
