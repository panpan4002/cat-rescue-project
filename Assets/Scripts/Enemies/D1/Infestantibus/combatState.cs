using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combatState : State
{
    [Header("State Machine")]
    public State infAttackState;
    public State infPersuePlayerState;

    [Header("Combat")]
    public int stageIndex = 1;

    public bool canRangeAttack;
    private float rangedAttackCDCounting;
    public float rangedAttackCDT = 8;

    public bool canMeleeAttack;
    private float meleeAttackCDCounting;
    public float meleeAttackCDT = 4;

    public bool canHitGroundAttack;
    private float hitGroundAttackCDCounting;
    public float hitGroundAttackCDT = 10;

    public int attackIndex;
    public cristalAreaCollider infCristalAreaCollider;

    [Header("Raycast")]
    private RaycastHit2D leftAttackRange;
    private RaycastHit2D rightAttackRange;
    public bool playerInMeleeRangeL, playerInMeleeRangeR;
    public int playerIsToRight;
    public Vector2 attackRangeOffSet;
    public LayerMask playerLayer;

    void Start()
    {
        
    }

    void Update()
    {
        CD();
    }

    public override State Tick(enemy stateEnemy, Animator enemyAnimator, Boss infestantibus)
    {
        if(infestantibus.Infestantibus.enemyLife <= (infestantibus.Infestantibus.enemyMaxLife / 2))
        {
            rangedAttackCDT = 4;
            meleeAttackCDT = 2;
            hitGroundAttackCDT = 6;

            stageIndex = 2;
        }

        else if(infestantibus.Infestantibus.enemyLife <= (infestantibus.Infestantibus.enemyMaxLife / 4))
        {
            rangedAttackCDT = 3;
            meleeAttackCDT = 1;
            hitGroundAttackCDT = 4;

            stageIndex = 3;
        }

        enemyAnimator.ResetTrigger("meleeAttackTrigger");
        //enemyAnimator.SetTrigger("combatTrigger");
        checkSurroundings();
        infestantibus.infestantibusRB.velocity = new Vector2(0, infestantibus.infestantibusRB.velocity.y);

        /*if (!playerInMeleeRangeL && !playerInMeleeRangeR)
        {
            return infPersuePlayerState;
        }*/

        if (Vector3.Distance(transform.position, infestantibus.playerScript.transform.position) <= 10 && canRangeAttack)
        {
            Debug.Log("range attack");
            canRangeAttack = false;
            attackIndex = 2;
            return infAttackState;
        }

        else if (playerInMeleeRangeL && canMeleeAttack)
        {
            Debug.Log("Melee attack L");
            canMeleeAttack = false;
            attackIndex = 0;
            return infAttackState;
        }

        else if (playerInMeleeRangeR && canMeleeAttack)
        {
            canMeleeAttack = false;
            Debug.Log("Melee attack R");
            attackIndex = 0;
            return infAttackState;
        }

        else if(infCristalAreaCollider.playerColliding && canHitGroundAttack && Vector3.Distance(transform.position, infestantibus.playerScript.transform.position) <= 15)
        {
            Debug.Log("cristal hit ground attack");
            canHitGroundAttack = false;
            attackIndex = 1;
            return infAttackState;
        }

        else if (!canMeleeAttack && !canRangeAttack && !canHitGroundAttack) enemyAnimator.SetTrigger("combatTrigger");

        else
        {
            return infPersuePlayerState;
        }
        return this;
    }

    void CD()
    {
        if (!canMeleeAttack)
        {
            meleeAttackCDCounting += Time.deltaTime;
            if (meleeAttackCDCounting >= meleeAttackCDT)
            {
                meleeAttackCDCounting = 0;
                canMeleeAttack = true;
            }
        }

        if (!canHitGroundAttack)
        {
            hitGroundAttackCDCounting += Time.deltaTime;
            if (hitGroundAttackCDCounting >= hitGroundAttackCDT)
            {
                hitGroundAttackCDCounting = 0;
                canHitGroundAttack = true;
            }
        }

        if (!canRangeAttack)
        {
            rangedAttackCDCounting += Time.deltaTime;
            if (rangedAttackCDCounting >= rangedAttackCDT)
            {
                rangedAttackCDCounting = 0;
                canRangeAttack = true;
            }
        }
    }

    void checkSurroundings()
    {
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
