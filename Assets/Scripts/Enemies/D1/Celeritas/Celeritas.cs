using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Celeritas : MonoBehaviour
{
    [Header("Components")]
    private enemy celeritasEnemy;
    private GameObject player;
    private Player playerScript;
    private Rigidbody2D celeritasRB;
    public golemGroundCheck celeritasGroundChecker;
    private Animator celeritasAnim;
    private SpriteRenderer celeritasSR;

    [Header("Particles")]
    public ParticleSystem bravo1, bravo2;
    public ParticleSystem atencao;

    [Header("Stats")]
    public float golemSpeed;
    private Vector3 playerPos = new Vector3(0, 0, 0);
    private int playerIsToRight = 1;
    private float golemYSpeed;
    public int golemFallDamage;
    public int golemDamage;

    private bool canJump;
    private float jumpCDCounting;
    private float jumpCDT = 5;
    private bool isJumping;

    private bool canAttack;
    private float attackCDCounting;
    public float attackCDT = 5;
    private bool isAttacking;

    private bool canDash;
    private float dashCDCounting;
    public float dashCDT = 10;
    private bool isDashing;

    float alpha = 1;
    bool fade = false;

    bool stopFallR;
    bool stopFallL;

    bool isFollowing;

    bool jumpTriggerR, jumpTriggerL;
    float jumpTriggerCounting;
    bool checkSurroundings = true;

    private int missedAttacks;

    public int attackIndex;

    /*[Header("Raycast")]
    private RaycastHit2D rightWall;
    private RaycastHit2D leftWall;
    private RaycastHit2D rightEdge;
    private RaycastHit2D leftEdge;

    public Vector2 wallOffSet;
    public Vector2 edgeOffSet;
    public LayerMask Ground;
    public LayerMask Wall;

    [Header("Animation")]
    private bool runningAnim;
    private bool attackingAnim;
    private bool dyingAnim;
    private bool idleAnim;
    private bool jumpingAnim;*/

    [Header("State Machine")]
    private celeritasIdleState celeritasIdle;
    [SerializeField]
    private celeritasState currentState;

    void Start()
    {
        celeritasEnemy = GetComponent<enemy>();
        celeritasRB = GetComponent<Rigidbody2D>();
        celeritasAnim = GetComponent<Animator>();
        celeritasSR = GetComponent<SpriteRenderer>();

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();

        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), true);

        celeritasIdle = GetComponent<celeritasIdleState>();
        currentState = celeritasIdle;
    }

    void Update()
    {
        if (!celeritasEnemy.isDead) executeStateMachine();

        if(celeritasEnemy.isDead)
        {
            celeritasAnim.SetTrigger("deadTrigger");
            StartCoroutine("Die");
            Color color = celeritasSR.color;
            celeritasSR.color = new Color(color.r, color.g, color.b, alpha);
            if(fade) alpha -= 0.0012f;
        }/*

        golemYSpeed = golemRB.velocity.y;

        playerPos = new Vector3(playerScript.transform.position.x, playerScript.transform.position.y, 0);

        if (!canJump)
        {
            jumpCDCounting += Time.deltaTime;
            if (jumpCDCounting >= jumpCDT)
            {
                jumpCDCounting = 0;
                canJump = true;
            }
        }

        if (!canAttack)
        {
            attackCDCounting += Time.deltaTime;
            if (attackCDCounting >= attackCDT)
            {
                attackCDCounting = 0;
                canAttack = true;
            }
        }

        if (!canDash)
        {
            dashCDCounting += Time.deltaTime;
            if (dashCDCounting >= dashCDT)
            {
                dashCDCounting = 0;
                canDash = true;
            }
        }

        //FallDamage();

        if (golemGroundChecker.fall) checkSurroundings = true;
        if (checkSurroundings) CheckSurroundings();

        celeritasAnimation();*/
    }

    void FixedUpdate()
    {
        /*if(!golem.isDead)
        {   
            //if(canJump && Vector3.Distance(transform.position, playerScript.transform.position) >= 2 && Vector3.Distance(transform.position, playerScript.transform.position) <= 6)
            //{
               // StartCoroutine(jump());
            //}

            //if(missedAttacks >= 6 && !isDashing && golemGroundChecker.isGrounded)
            //{
            //   if(canDash)
            //   {
            //       missedAttacks = 0;
            //        StartCoroutine(dashAttack());
            //    }
            //}

            if (canAttack && Mathf.Round(transform.position.x) == Mathf.Round(playerPos.x) && Vector3.Distance(transform.position, playerScript.transform.position) <= 3)
            {
                StartCoroutine(attackCoroutine());
            }

            else if(Vector3.Distance(transform.position, playerScript.transform.position) <= 6 && Vector3.Distance(transform.position, playerScript.transform.position) >= 2)
            {
                if(!stopFallL && !stopFallR && !jumpTriggerL && !jumpTriggerR && !isAttacking && !isDashing)
                {
                    followPlayer();
                }

                else if(!isDashing)
                {
                    golemRB.velocity = new Vector2(0, 0);
                    isFollowing = false;
                }
            }
        }

        if(jumpTriggerR || jumpTriggerL)
        {
            jumpTriggerCounting += Time.deltaTime;
            Debug.Log("golem deve pular");
            Debug.Log(jumpTriggerCounting);

            if(jumpTriggerCounting >= 1)
            {
                if(canJump) StartCoroutine(simpleJump());
            }
        }

        else
        {
            jumpTriggerCounting = 0;
        }*/
    }

    void LateUpdate()
    {
        //stopAnim();
    }
    private void executeStateMachine()
    {
        if (currentState != null)
        {
            celeritasState nextState = currentState.Tick(celeritasEnemy, celeritasAnim, this);

            if (nextState != null)
            {
                toNextState(nextState);
            }
        }
    }

    private void toNextState(celeritasState state)
    {
        currentState = state;
    } /*

    void stopAnim()
    {
        if (runningAnim) runningAnim = false;
        if (attackingAnim) attackingAnim = false;
        if (jumpingAnim) jumpingAnim = false;
        if (idleAnim) idleAnim = false;
        if (dyingAnim) dyingAnim = false;
    }

    void celeritasAnimation()
    {
        if(isAttacking) attackingAnim = true;
        if (isFollowing) runningAnim = true;
        else if (!isFollowing) runningAnim = false; 

        if(!golem.isDead)
        {
            if (attackingAnim) this.animator.SetTrigger("isAttacking");
        }

        else
        {           
            if (dyingAnim) this.animator.SetTrigger("isDying");
        }
    }
    void CheckSurroundings()
    {
        rightEdge = Physics2D.Raycast(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 2.5f, Ground);
        Debug.DrawRay(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down * 2.5f, Color.yellow);

        if (rightEdge.collider == null && playerIsToRight == 1)
        {
            if (golemGroundChecker.isGrounded == true) stopFallR = true;
            bravo2.Play();
            Debug.Log("deve parar");
        }

        else stopFallR = false;

        leftEdge = Physics2D.Raycast(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 2.5f, Ground);
        Debug.DrawRay(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down * 2.5f, Color.yellow);

        if (leftEdge.collider == null && playerIsToRight == -1)
        {
            if (golemGroundChecker.isGrounded == true) stopFallR = true;
            bravo2.Play();
            Debug.Log("deve parar");
        }

        else stopFallL = false;

        rightWall = Physics2D.Raycast(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right, 1f, Wall);
        Debug.DrawRay(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right, Color.red);

        if (rightWall.collider != null && playerIsToRight == 1)
        {
            jumpTriggerR = true;
        }

        else jumpTriggerR = false;

        leftWall = Physics2D.Raycast(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left, 1f, Wall);
        Debug.DrawRay(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left, Color.red);

        if (leftWall.collider != null && playerIsToRight == -1)
        {
            jumpTriggerL = true;
        }

        else jumpTriggerL = false;
    }

    IEnumerator simpleJump()
    {
        isJumping = true;
        checkSurroundings = false;
        jumpTriggerL = false;
        jumpTriggerR = false;
        yield return new WaitForSeconds(.3f);
        golemRB.AddForce(new Vector2(playerIsToRight * 5, 12), ForceMode2D.Impulse);
        canJump = false;
        yield return "pulou";
    }

    IEnumerator jump()
    {
        isJumping = true;
        canJump = false;
        golemRB.velocity = new Vector2(0, 0);
        golemRB.gravityScale = 0;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + playerIsToRight, transform.position.y + 3), 1);
        yield return new WaitForSeconds(1f);
        golemRB.gravityScale = 1;   
    }

    IEnumerator dashAttack()
    {
        bravo2.Play();
        golemRB.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(.3f);
        Debug.Log("DASH");
        canDash = false;
        isDashing = true;
        golemRB.AddForce(new Vector2(20 * playerIsToRight, 0), ForceMode2D.Impulse);
        yield return new WaitForSeconds(.3f);
        isDashing = false;
    }

    IEnumerator attackCoroutine()
    {
        golemRB.velocity = new Vector2(0, 0);
        playerScript.recoveryTime = 0;
        canAttack = false;
        isAttacking = true;
        yield return new WaitForSeconds(.3f);
        if (Mathf.Round(transform.position.x) == Mathf.Round(playerPos.x) && Vector3.Distance(transform.position, playerScript.transform.position) <= 3)
        {
            playerScript.playerTakeDamageAUX(false, golemDamage);
            Debug.Log("bateu 1");
        }
        else
        {
            missedAttacks++;
            bravo1.Play();
            Debug.Log("errou 1");
        }
        yield return new WaitForSeconds(.3f);
        if (Mathf.Round(transform.position.x) == Mathf.Round(playerPos.x) && Vector3.Distance(transform.position, playerScript.transform.position) <= 3)
        {
            playerScript.playerTakeDamageAUX(true, golemDamage);
            bravo1.Play();
            Debug.Log("bateu 2");
        }
        else
        {
            missedAttacks++;
            Debug.Log("errou 2");
        }
        isAttacking = false;
        playerScript.recoveryTime = 1;
    }

    void FallDamage()
    {
        if(golemGroundChecker.fall)  
        {
        
        }
    }

    void followPlayer()
    {
        Vector2 speedRB = golemRB.velocity;
        speedRB.x = golemSpeed * playerIsToRight;
        golemRB.velocity = speedRB;
        isFollowing = true;
    }

*/

    IEnumerator Die()
    {
        celeritasRB.velocity = new Vector2(0, celeritasRB.velocity.y);
        yield return new WaitForSeconds(1);
        fade = true;
        yield return new WaitForSeconds(8f);

        Destroy(gameObject);
        fade = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //if(isDashing) playerScript.playerTakeDamage(golemDamage);
        }
    }
}
