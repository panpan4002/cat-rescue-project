using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class Boss : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;
    public Player playerScript;
    public Rigidbody2D infestantibusRB;
    public Animator infestantibusAnimator;
    public BoxCollider2D infestantibusCol;
    public enemy Infestantibus;
    public GameObject lifeBarCanvas;
    public Slider lifeBar;
    public TextMeshProUGUI lifeLifebar;

    [Header("Movement")]
    public float infestantibusSpeed;
    private int direction;

    [Header("Combat")]
    public int meleeDamage;

    public int rangedDamage;
    public GameObject projectile;
    public GameObject projectileShooter;
    

    private int playerIsToRight;
    private bool playerInMeleeRangeL, playerInMeleeRangeR;

    private bool risadinha;

    private Vector2 playerPos = new Vector2(0, 0);

    [Header("Animation")]
    private bool isDying;
    private bool isFollowing;

    [Header("Raycast")]

    [Header("StateMachine")]
    public State currentState;

    void Start()
    {
        Infestantibus = GetComponent<enemy>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        infestantibusRB = GetComponent<Rigidbody2D>();
        infestantibusAnimator = GetComponent<Animator>();
        infestantibusCol = GetComponent<BoxCollider2D>();
        lifeBar.maxValue = Infestantibus.enemyMaxLife;

        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>(), true);


    }

    void Update()
    {
        lifeLifebar.text = Infestantibus.enemyLife + " / " + Infestantibus.enemyMaxLife;
        lifeBar.value = Infestantibus.enemyLife;
        if (Infestantibus.isDead)
        {
            lifeBarCanvas.SetActive(false);
        }

        if(!Infestantibus.isDead) executeStateMachine();
        anim();
        /*if (!Infestantibus.isDead)
        {
            if (transform.position.x > playerPos.x)
            {
                playerIsToRight = -1;
            }
            else
            {
                playerIsToRight = 1;
            }
        }


        
        CheckSurroundings();

        playerPos = playerScript.transform.position;
        */
    }

    private void FixedUpdate()
    {
        /*if (!Infestantibus.isDead)
        {
            if (infestantibusRB.velocity.x > 0) transform.eulerAngles = new Vector2(0f, 180f);
            else if (infestantibusRB.velocity.x < 0) transform.eulerAngles = new Vector2(0f, 0f);

            if (!playerScript.dead)
            {
                if (playerInMeleeRangeL || playerInMeleeRangeR)
                {
                    if (risadinha) StartCoroutine(meleeAttack());
                }

                else if (Vector3.Distance(transform.position, playerPos) <= 8 && !isMeleeAttacking && !isRangeAttacking)
                {
                    StartCoroutine(esperaRisadinha());
                    if (risadinha) followPlayer();
                }

                else
                {
                    risadinha = false;
                    isFollowing = false;
                    infestantibusRB.velocity = new Vector2(0, infestantibusRB.velocity.y);
                }
            }

            else

            {
                risadinha = false;
                isFollowing = false;
                infestantibusRB.velocity = new Vector2(0, infestantibusRB.velocity.y);
            }
        }*/
    }

    private void executeStateMachine()
    {
        if(currentState != null)
        {
            State nextState = currentState.Tick(Infestantibus, infestantibusAnimator, this);

            if (nextState != null)
            {
                toNextState(nextState);
            }
        }
    }

    private void toNextState(State state)
        {
            currentState = state;
        }

    void LateUpdate()
    {

    }

    void stopAnim()
    {
       // if (isDying) isDying = false; Debug.Log("paro o sexo");
    }

    void anim()
    {
        if (Infestantibus.isDead) infestantibusAnimator.SetTrigger("deadTrigger");

        //if (isDying) infestantibusAnimator.SetTrigger("deathAnim"); Debug.Log("sexo");

        //if (isFollowing) infestantibusAnimator.SetBool("isFollowing", true);
        //else infestantibusAnimator.SetBool("isFollowing", false);

        //if (isMeleeAttacking) infestantibusAnimator.SetTrigger("meleeAttackTrigger");
    }

    IEnumerator esperaRisadinha()
    {
        isFollowing = true;
        yield return new WaitForSeconds(1.3f);
        risadinha = true;
    }

    /*IEnumerator meleeAttack()
    {
        isMeleeAttacking = true;
        canMeleeAttack = false;
        yield return new WaitForSeconds(0.3f);
        if (playerInMeleeRangeL || playerInMeleeRangeR) playerScript.playerTakeDamageAUX(true, meleeDamage);
        isMeleeAttacking = false;
    }

    IEnumerator rangedAttack()
    {
        isRangeAttacking = true;
        canRangeAttack = false;
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile1 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile2 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile3 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile4 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
        yield return new WaitForSeconds(.3f);
        GameObject instantiatedProjectile5 = Instantiate(projectile, projectileShooter.transform.position, transform.rotation);
        isRangeAttacking = false;
    }*/

}
