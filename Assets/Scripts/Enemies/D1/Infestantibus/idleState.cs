using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleState : State
{
    [Header("State Machine")]
    public persuePlayerState infPersuePlayerState;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override State Tick(enemy stateEnemy, Animator enemyAnimator, Boss infestantibus)
    {
        if(!infestantibus.playerScript.dead)
        {
            if (Vector3.Distance(infestantibus.transform.position, infestantibus.playerScript.transform.position) <= 10 && !infestantibus.playerScript.dead)
            {
                Debug.Log("achou jogador");
                Debug.Log("deve perseguir");
                return infPersuePlayerState;
            }

            else
            {
                return this;
            }
        }
        else
        {
            return this;
        }
       
    }
}
