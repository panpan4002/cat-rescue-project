using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caput : MonoBehaviour
{
    [Header("Components")]
    private enemy caput;
    private Rigidbody2D caputRB;
    private CapsuleCollider2D caputCol;
    private Animator caputAnimator;
    private SpriteRenderer caputSR;
    private Color caputColor;
    private GameObject player;
    private Player playerScript;

    [Header("Stats")]
    public int caputDMG;
    public float caputSpeed;

    private bool canAttack;
    private float attackCDCounting;
    public float attackCDT = 2;
    private bool isAttacking;

    private bool isFollowing;
    private bool attackAnim;
    private bool deathAnim;

    private float alpha = 1;

    private Vector3 playerPos;
    private int playerIsToRight;

    private bool playerInRangeR, playerInRangeL;

    [Header("Raycast")]
    private RaycastHit2D rightWall;
    private RaycastHit2D leftWall;
    private RaycastHit2D rightEdge;
    private RaycastHit2D leftEdge;

    private RaycastHit2D leftAttackRange;
    private RaycastHit2D rightAttackRange;

    private bool stopWallL, stopWallR, stopEdgeL, stopEdgeR;

    public Vector2 wallOffSet;
    public Vector2 edgeOffSet;
    public Vector2 attackRangeOffSet;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask playerLayer;

    void Start()
    {
        caput = GetComponent<enemy>();
        caputRB = GetComponent<Rigidbody2D>();
        caputCol = GetComponent<CapsuleCollider2D>();
        caputAnimator = GetComponent<Animator>();
        caputSR = GetComponent<SpriteRenderer>();
        caputColor = caputSR.color;

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), caputCol, true);

        isFollowing = false;
    }

    void Update()
    {
        CheckSurroundings();
        caputAnim();

        if (caput.isDead)
        {
            StartCoroutine("Die");
            caputSR.color = new Color(caputColor.r, caputColor.g, caputColor.b, alpha);
            alpha -= 0.0012f;
        }

        playerPos = playerScript.transform.position;

        if (!caput.isDead)
        {
            if (transform.position.x > playerScript.transform.position.x)
            {
                //transform.eulerAngles = new Vector2(0f, 0);
                playerIsToRight = -1;
            }
            else
            {
                playerIsToRight = 1;
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
    }   

    void FixedUpdate()
    {
        if (!caput.isDead)
        {
            if (caputRB.velocity.x > 0) transform.eulerAngles = new Vector2(0f, 180f);
            else if (caputRB.velocity.x < 0) transform.eulerAngles = new Vector2(0f, 0f);

            if (!playerScript.dead)
            {
                if (playerInRangeL || playerInRangeR)
                {
                    if (canAttack) StartCoroutine(attackCoroutine());
                }
                //else if (!isAttacking && !stopL && !stopR && !playerInRangeL && !playerInRangeR) followPlayer();
                else if (Vector3.Distance(transform.position, playerPos) <= 8 && !isAttacking)
                {
                    followPlayer();
                }
                else
                {
                    caputRB.velocity = new Vector2(0, caputRB.velocity.y);
                    isFollowing = false;
                }
            }

            else

            {
                isFollowing = false;
                caputRB.velocity = new Vector2(0, caputRB.velocity.y);
            }
        }
    }

    void LateUpdate()
    {
        if (attackAnim) attackAnim = false;   
    }

    void caputAnim()
    {
        if (!caput.isDead)
        {
            if (attackAnim) caputAnimator.SetTrigger("attackTrigger");
            else if (isFollowing) caputAnimator.SetBool("following", true);
            else caputAnimator.SetBool("following", false);
        }

        else
        {
            caputAnimator.SetTrigger("deadTrigger");
        }
    }

    void followPlayer()
    {
        //if (Vector3.Distance(transform.position, playerPos) <= 8 && Vector3.Distance(transform.position, playerPos) >= 1.5f && !isAttacking)
        if(!stopWallL && !stopWallR && !stopEdgeL && !stopEdgeR && !playerInRangeL && !playerInRangeR)
        {
            Vector2 speedRB = caputRB.velocity;
            speedRB.x = caputSpeed * playerIsToRight;
            caputRB.velocity = speedRB;
            isFollowing = true;
        }

        else

        {
            caputRB.velocity = new Vector2(0, caputRB.velocity.y);
            isFollowing = false;
        }
    }

    IEnumerator attackCoroutine()
    {
        isAttacking = true;
        canAttack = false;
        attackAnim = true;
        caputRB.velocity = new Vector2(0, caputRB.velocity.y);
        yield return new WaitForSeconds(.5f);
        if (playerInRangeL || playerInRangeR) playerScript.playerTakeDamageAUX(true, caputDMG);
        else
        {
            GameObject floatingMissInstantiated = Instantiate(caput.floatingDamage, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            floatingMissInstantiated.GetComponentInChildren<TextMesh>().fontSize = 250;
            floatingMissInstantiated.GetComponentInChildren<TextMesh>().text = "ERROU!";
        }
        isAttacking = false;
    }

    IEnumerator Die()
    {
        caputRB.velocity = new Vector2(0, 0);
        //Destroy(caputCol);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void CheckSurroundings()
    {
        rightEdge = Physics2D.Raycast(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 2f, groundLayer);
        Debug.DrawRay(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down * 2f, Color.yellow);

        if (rightEdge.collider == null && playerIsToRight == 1)
        {
            stopEdgeR = true;
        }

        else stopEdgeR = false;

        leftEdge = Physics2D.Raycast(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 2f, groundLayer);
        Debug.DrawRay(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down * 2f, Color.yellow);

        if (leftEdge.collider == null && playerIsToRight == -1)
        {
            stopEdgeL = true;
        }

        else stopEdgeL = false;

        rightWall = Physics2D.Raycast(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right, 1f, wallLayer);
        Debug.DrawRay(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right * 1, Color.blue);

        if (rightWall.collider != null && playerIsToRight == 1)
        {
            stopWallR = true;
        }

        else stopWallR = false;


        leftWall = Physics2D.Raycast(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left, 1f, wallLayer);
        Debug.DrawRay(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left * 1, Color.blue);

        if (leftWall.collider != null && playerIsToRight == -1)
        {
            stopWallL = true;
        }

        else stopWallL = false;

        rightAttackRange = Physics2D.Raycast(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right, 1f, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right * 1, Color.red);

        if (rightAttackRange.collider != null) playerInRangeR = true;
        else playerInRangeR = false;

        leftAttackRange = Physics2D.Raycast(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left, 1, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left * 1, Color.red);

        if (leftAttackRange.collider != null) playerInRangeL = true;
        else playerInRangeL = false;
    }
}
