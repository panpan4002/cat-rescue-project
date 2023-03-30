using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class celeritasState : MonoBehaviour
{
    public abstract celeritasState Tick(enemy celeritasEnemy, Animator celeritasEnemyAnimator, Celeritas celeritasEnemyScript);
}
