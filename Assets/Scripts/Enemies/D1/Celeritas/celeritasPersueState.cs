using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celeritasPersueState : celeritasState
{
    [Header("State Machine")]
    private celeritasIdleState celeritasIdle;
    private celeritasCombatState celeritasCombat;

    [Header("Components")]
    private enemy celeritasEnemy;
    private GameObject player;
    private Player playerScript;
    private Rigidbody2D celeritasRB;
    //public golemGroundCheck celeritasGroundChecker;
    private Animator celeritasAnim;
    private SpriteRenderer celeritasSR;

    [Header("Movement")]
    private int playerIsToRight;
    public float celeritasSpeed;

    [Header("Raycast")]
    private RaycastHit2D rightWall;
    private RaycastHit2D leftWall;
    private RaycastHit2D rightEdge;
    private RaycastHit2D leftEdge;
    private RaycastHit2D leftAttackRange;
    private RaycastHit2D rightAttackRange;

    private bool stopWallL, stopWallR, stopEdgeL, stopEdgeR;
    private bool playerInMeleeRangeL, playerInMeleeRangeR;

    public Vector2 wallOffSet;
    public Vector2 edgeOffSet;
    public Vector2 attackRangeOffSet;

    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public LayerMask playerLayer;

    private void Start()
    {
        celeritasEnemy = GetComponent<enemy>();
        celeritasRB = GetComponent<Rigidbody2D>();
        celeritasAnim = GetComponent<Animator>();
        celeritasSR = GetComponent<SpriteRenderer>();

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();

        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), true);

        celeritasIdle = GetComponent<celeritasIdleState>();
        celeritasCombat = GetComponent<celeritasCombatState>();
    }

    private void Update()
    {

    }

    public override celeritasState Tick(enemy celeritasEnemy, Animator celeritasEnemyAnimator, Celeritas celeritasEnemyScript)
    {
        if (transform.position.x > playerScript.transform.position.x)
        {
            if (!celeritasEnemy.isDead) transform.eulerAngles = new Vector2(0f, 0);
            playerIsToRight = -1;
        }
        else
        {
            if (!celeritasEnemy.isDead) transform.eulerAngles = new Vector2(0f, 180f);
            playerIsToRight = 1;
        }


        if (Vector3.Distance(transform.position, playerScript.transform.position) >= 8)
        {
            celeritasAnim.SetBool("isFollowing", false);
            celeritasRB.velocity = new Vector2(0, celeritasRB.velocity.y);
            return celeritasIdle;
        }

        else
        {
            CheckSurroundings();

            if (!stopWallL && !stopWallR && !stopEdgeL && !stopEdgeR && !playerInMeleeRangeL && !playerInMeleeRangeR)
            {
                celeritasAnim.SetBool("isFollowing", true);
                Vector2 speedRB = celeritasRB.velocity;
                speedRB.x = celeritasSpeed * playerIsToRight;
                celeritasRB.velocity = speedRB;
            }

            else if (playerInMeleeRangeL || playerInMeleeRangeR)
            {
                celeritasAnim.SetBool("isFollowing", false);
                celeritasRB.velocity = new Vector2(0, celeritasRB.velocity.y);
                //audios[0].Stop();
                Debug.Log("sexo");
                return celeritasCombat;
            }
        }

        return this;
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

        rightAttackRange = Physics2D.Raycast(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right, 1, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right * 1, Color.red);

        if (rightAttackRange.collider != null) playerInMeleeRangeR = true;
        else playerInMeleeRangeR = false;

        leftAttackRange = Physics2D.Raycast(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left, 1, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left * 1, Color.red);

        if (leftAttackRange.collider != null) playerInMeleeRangeL = true;
        else playerInMeleeRangeL = false;
    }
}
