using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    //public enemy stateEnemy;
    //public Animator enemyAnimator;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public abstract State Tick(enemy stateEnemy, Animator enemyAnimator, Boss infestantibus);
}
