using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celeritasCombatState : celeritasState
{
    [Header("State Machine")]
    private celeritasPersueState celeritasPersue;
    private celeritasAttackState celeritasAttack;

    [Header("Components")]
    private enemy celeritasEnemy;
    private GameObject player;
    private Player playerScript;
    private Rigidbody2D celeritasRB;
    //public golemGroundCheck celeritasGroundChecker;
    private Animator celeritasAnim;
    private SpriteRenderer celeritasSR;

    [Header("CoolDown")]
    private bool canAttack;
    private float attackCDCounting;
    public float attackCDT = 5;
    private bool isAttacking;

    [Header("Raycast")]
    private RaycastHit2D leftAttackRange;
    private RaycastHit2D rightAttackRange;

    public bool playerInMeleeRangeL, playerInMeleeRangeR;

    public Vector2 attackRangeOffSet;

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

        celeritasPersue = GetComponent<celeritasPersueState>();
        celeritasAttack = GetComponent<celeritasAttackState>();

        canAttack = true;
    }

    public override celeritasState Tick(enemy celeritasEnemy, Animator celeritasEnemyAnimator, Celeritas celeritasEnemyScript)
    {
        Debug.Log("comcbatstate");
        CD();
        CheckSurroundings();

        if(playerInMeleeRangeR)
        {
            if(canAttack)
            {
                celeritasEnemyScript.attackIndex = 0;
                canAttack = false;
                return celeritasAttack;
            }

            else
            {
                return this;
            }
        }

        if (playerInMeleeRangeL)
        {
            if (canAttack)
            {
                celeritasEnemyScript.attackIndex = 1;
                canAttack = false;
                return celeritasAttack;
            }

            else
            {
                return this;
            }
        }

        else if(!playerInMeleeRangeL && !playerInMeleeRangeR)
        {
            return celeritasPersue;
        }

        return this;
    }
    
    void CD()
    {
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

    void CheckSurroundings()
    {
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
