using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persuePlayerState : State
{
    [Header("State Machine")]
    public idleState infIdleState;
    public combatState infCombatState;

    [Header("Movement")]
    private Rigidbody2D infestantibusRB;
    public float speed;
    public int playerIsToRight = -1;

    public cristalAreaCollider infCristalAreaCollider;

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

    public AudioSource[] audios;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override State Tick(enemy stateEnemy, Animator enemyAnimator, Boss infestantibus)
    {
        //Debug.Log(Vector3.Distance(transform.position, infestantibus.playerScript.transform.position));
        infestantibusRB = GetComponent<Rigidbody2D>();

        if(Vector3.Distance(transform.position, infestantibus.playerScript.transform.position) >= 10 && !infestantibus.playerScript.dead)
        {
            enemyAnimator.SetBool("isFollowing", false);
            return infIdleState;
        }

        else

        {
            CheckSurroundings();

            //playerIsToRight = transform.position.x <= infestantibus.playerScript.transform.position.x ? 1 : -1;

            if (transform.position.x <= infestantibus.playerScript.transform.position.x)
            {
                playerIsToRight = 1;
                if (!infCombatState.GetComponent<attackState>().isAttacking)
                {
                    transform.eulerAngles = new Vector2(0, 180);
                }
            }
            else
            {
                playerIsToRight = -1;
                if (!infCombatState.GetComponent<attackState>().isAttacking)
                {
                    transform.eulerAngles = new Vector2(0, 0);
                }
            }

            if (!stopWallL && !stopWallR && !stopEdgeL && !stopEdgeR && !playerInMeleeRangeL && !playerInMeleeRangeR)
            {
                //audios[0].Play();
                enemyAnimator.SetBool("isFollowing", true);
                Vector2 speedRB = infestantibusRB.velocity;
                speedRB.x = speed * playerIsToRight;
                infestantibusRB.velocity = speedRB;
                //infestantibusRB.velocity = new Vector2(infestantibusRB.velocity.x * speed, infestantibusRB.velocity.y);
                Debug.Log("sexo");
            }

            else if(playerInMeleeRangeL || playerInMeleeRangeR)
            {
                enemyAnimator.SetBool("isFollowing", false);
                infestantibusRB.velocity = new Vector2(0, infestantibusRB.velocity.y);
                //audios[0].Stop();
                return infCombatState;
            }

            else if(Vector3.Distance(transform.position, infestantibus.playerScript.transform.position) <= 15 && infCristalAreaCollider.playerColliding)
            {
                enemyAnimator.SetBool("isFollowing", false);
                infestantibusRB.velocity = new Vector2(0, infestantibusRB.velocity.y);
                //audios[0].Stop();
                return infCombatState;
            }

            /*else if (Vector3.Distance(transform.position, infestantibus.playerScript.transform.position) <= 15)
            {
                enemyAnimator.SetBool("isFollowing", false);
                infestantibusRB.velocity = new Vector2(0, infestantibusRB.velocity.y);
                return infCombatState;
            }*/

            else

            {
                enemyAnimator.SetBool("isFollowing", false);
                //risadinha = false;
                infestantibus.infestantibusRB.velocity = new Vector2(0, infestantibus.infestantibusRB.velocity.y);
                //audios[0].Stop();
                //isFollowing = false;
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

        rightAttackRange = Physics2D.Raycast(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right, 3, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right * 3, Color.red);

        if (rightAttackRange.collider != null) playerInMeleeRangeR = true;
        else playerInMeleeRangeR = false;

        leftAttackRange = Physics2D.Raycast(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left, 3, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left * 3, Color.red);

        if (leftAttackRange.collider != null) playerInMeleeRangeL = true;
        else playerInMeleeRangeL = false;
    }
}
