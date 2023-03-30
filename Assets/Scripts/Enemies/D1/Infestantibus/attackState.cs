using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackState : State
{
    [Header("State Machine")]
    public combatState infCombatState;

    public bool isAttacking;
    public bool isMeleeAttacking;

    public GameObject fallingSpike;
    public GameObject projectileShooter;
    public GameObject projectile;

    Boss Infestantibus;

    public AudioSource[] audios;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override State Tick(enemy stateEnemy, Animator enemyAnimator, Boss infestantibus)
    {
        Infestantibus = infestantibus;

        switch (infCombatState.attackIndex)
        {
            case 0:
                enemyAnimator.SetTrigger("meleeAttackTrigger");
                infCombatState.canMeleeAttack = false;
                StartCoroutine(meleeAttack());
                //meleeAttack();
                //enemyAnimator.ResetTrigger("meleeAttackTrigger");
                return infCombatState;

            case 1:
                enemyAnimator.SetTrigger("hitGroundTrigger");
                StartCoroutine(cristalHitGroundAttack());
                return infCombatState;

            case 2:
                enemyAnimator.SetTrigger("rangedAttackTrigger");
                StartCoroutine(rangedAttack());
                return infCombatState;

            case 3:
                return infCombatState;
        }

        return infCombatState;
    }

    IEnumerator meleeAttack()
    {
        isMeleeAttacking = true;
        Infestantibus.infestantibusRB.velocity = new Vector2(0, Infestantibus.infestantibusRB.velocity.y);
        yield return new WaitForSeconds(0.1f);
        if (infCombatState.playerInMeleeRangeL || infCombatState.playerInMeleeRangeR) Infestantibus.playerScript.playerTakeDamageAUX(true, Infestantibus.meleeDamage);
        yield return new WaitForSeconds(0.2f);
        isMeleeAttacking = false;
    }

    IEnumerator cristalHitGroundAttack()
    {
        audios[2].Play();
        yield return new WaitForSeconds(1.8f);
        Infestantibus.playerScript.cameraShake(2f);
        audios[0].Play();
        audios[1].Play();

        GameObject instantiatedFallingSpike6 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x + 3, infCombatState.infCristalAreaCollider.transform.position.y + 18), Quaternion.identity);
        GameObject instantiatedFallingSpike2 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x - 1, infCombatState.infCristalAreaCollider.transform.position.y + 10), Quaternion.identity); 
        GameObject instantiatedFallingSpike4 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x + 1, infCombatState.infCristalAreaCollider.transform.position.y + 14), Quaternion.identity);
        
        if (infCombatState.stageIndex >= 2)
        {
            GameObject instantiatedFallingSpike1 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x - 2, infCombatState.infCristalAreaCollider.transform.position.y + 8), Quaternion.identity);
            GameObject instantiatedFallingSpike3 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x - 0, infCombatState.infCristalAreaCollider.transform.position.y + 12), Quaternion.identity);
            GameObject instantiatedFallingSpike5 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x + 2, infCombatState.infCristalAreaCollider.transform.position.y + 16), Quaternion.identity);
        }
        
        if(infCombatState.stageIndex >= 3)
        {
            GameObject instantiatedFallingSpike7 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x + 3, infCombatState.infCristalAreaCollider.transform.position.y + 10), Quaternion.identity);
            GameObject instantiatedFallingSpike8 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x + 2, infCombatState.infCristalAreaCollider.transform.position.y + 12), Quaternion.identity);
            GameObject instantiatedFallingSpike9 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x + 1, infCombatState.infCristalAreaCollider.transform.position.y + 14), Quaternion.identity);
            GameObject instantiatedFallingSpike10 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x - 0, infCombatState.infCristalAreaCollider.transform.position.y + 16), Quaternion.identity);
            GameObject instantiatedFallingSpike11 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x - 1, infCombatState.infCristalAreaCollider.transform.position.y + 18), Quaternion.identity);
            GameObject instantiatedFallingSpike12 = Instantiate(fallingSpike, new Vector2(infCombatState.infCristalAreaCollider.transform.position.x - 2, infCombatState.infCristalAreaCollider.transform.position.y + 20), Quaternion.identity);
        }

    }

    IEnumerator rangedAttack()
    {
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile1 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile2 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile3 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
    }
}
