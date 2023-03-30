using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class celeritasIdleState : celeritasState
{
    [Header("State Machine")]
    private celeritasPersueState celeritasPersue;

    [Header("Components")]
    private enemy celeritasEnemy;
    private GameObject player;
    private Player playerScript;
    private Rigidbody2D celeritasRB;
    //public golemGroundCheck celeritasGroundChecker;
    private Animator celeritasAnim;
    private SpriteRenderer celeritasSR;

    void Start()
    {
        celeritasEnemy = GetComponent<enemy>();
        celeritasRB = GetComponent<Rigidbody2D>();
        celeritasAnim = GetComponent<Animator>();
        celeritasSR = GetComponent<SpriteRenderer>();

        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();

        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), true);

        celeritasPersue = GetComponent<celeritasPersueState>();
    }

    void Update()
    {

    }

    public override celeritasState Tick(enemy celeritasEnemy, Animator celeritasEnemyAnimator, Celeritas celeritasEnemyScript)
    {
        if (transform.position.x > playerScript.transform.position.x)
        {
            if (!celeritasEnemy.isDead) transform.eulerAngles = new Vector2(0f, 0);
            //playerIsToRight = -1;
        }
        else
        {
            if (!celeritasEnemy.isDead) transform.eulerAngles = new Vector2(0f, 180f);
            //playerIsToRight = 1;
        }

        if (Vector3.Distance(transform.position, playerScript.transform.position) <= 8 && !playerScript.dead)
        {
            return celeritasPersue;
        }

        else
        {
            return this;
        }
    }
}
