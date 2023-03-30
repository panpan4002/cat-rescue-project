using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celeritasAttackState : celeritasState
{
    [Header("State Machine")]
    private celeritasCombatState celeritasCombat;

    [Header("Components")]
    private enemy celeritasEnemy;
    private GameObject player;
    private Player playerScript;
    private Rigidbody2D celeritasRB;
    //public golemGroundCheck celeritasGroundChecker;
    private Animator celeritasAnim;
    private SpriteRenderer celeritasSR;

    [Header("Combat")]
    public int celeritasMeleeDamage;

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

        celeritasCombat = GetComponent<celeritasCombatState>();
    }
    public override celeritasState Tick(enemy celeritasEnemy, Animator celeritasEnemyAnimator, Celeritas celeritasEnemyScript)
    {
        CheckSurroundings();

        switch (celeritasEnemyScript.attackIndex)
        {
            case 0:
                Debug.Log("em teoria ta batendo R");
                StartCoroutine(attackCoroutineR());
                return celeritasCombat;

            case 1:
                Debug.Log("em teoria ta batendo L");
                StartCoroutine(attackCoroutineL());
                return celeritasCombat;
        }
        return this;
    }

    IEnumerator attackCoroutineR()
    {
        celeritasAnim.SetTrigger("attackTrigger");
        celeritasRB.velocity = new Vector2(0, celeritasRB.velocity.y);
        playerScript.recoveryTime = 0;

        yield return new WaitForSeconds(.3f);
        if (playerInMeleeRangeR)
        {
            playerScript.playerTakeDamageAUX(false, celeritasMeleeDamage);
            Debug.Log("bateu 1");
        }
        else
        {
            //missedAttacks++;
            Debug.Log("errou 1");
        }
        yield return new WaitForSeconds(.3f);
        if (playerInMeleeRangeR)
        {
            playerScript.playerTakeDamageAUX(true, celeritasMeleeDamage);
            Debug.Log("bateu 2");
        }
        else
        {
            //missedAttacks++;
            Debug.Log("errou 2");
        }
        playerScript.recoveryTime = 1;
    }

    IEnumerator attackCoroutineL()
    {
        celeritasAnim.SetTrigger("attackTrigger");
        celeritasRB.velocity = new Vector2(0, celeritasRB.velocity.y);
        playerScript.recoveryTime = 0;

        yield return new WaitForSeconds(.3f);
        if (playerInMeleeRangeL)
        {
            playerScript.playerTakeDamageAUX(false, celeritasMeleeDamage);
            Debug.Log("bateu 1");
        }
        else
        {
            //missedAttacks++;
            Debug.Log("errou 1");
        }
        yield return new WaitForSeconds(.3f);
        if (playerInMeleeRangeL)
        {
            playerScript.playerTakeDamageAUX(true, celeritasMeleeDamage);
            Debug.Log("bateu 2");
        }
        else
        {
            //missedAttacks++;
            Debug.Log("errou 2");
        }
        playerScript.recoveryTime = 1;
    }

    void CheckSurroundings()
    {
        rightAttackRange = Physics2D.Raycast(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right, 1f, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x + attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.right * 1f, Color.red);

        if (rightAttackRange.collider != null) playerInMeleeRangeR = true;
        else playerInMeleeRangeR = false;

        leftAttackRange = Physics2D.Raycast(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left, 1, playerLayer);
        Debug.DrawRay(new Vector2(transform.position.x - attackRangeOffSet.x, transform.position.y + attackRangeOffSet.y), Vector2.left * 1f, Color.red);

        if (leftAttackRange.collider != null) playerInMeleeRangeL = true;
        else playerInMeleeRangeL = false;
    }
}
