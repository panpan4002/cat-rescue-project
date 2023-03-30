using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushGolem : MonoBehaviour
{
    [Header("Components")]
    private GameObject player;
    private Player playerScript;
    private Animator animator;
    public GameObject loot;
    public GameObject hand;
    private bool lootCD;
    public float lootCDTime;
    private float lootCDTimeCounting;

    private bool playerIsColliding;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        animator = GetComponent<Animator>();
        lootCD = false;
    }

    void Update()
    {
        if(playerIsColliding)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Submit"))
            {
                StartCoroutine(waitGive());
            }
        }

        if (lootCD)
        {
            lootCDTimeCounting += Time.deltaTime;
            if (lootCDTimeCounting >= lootCDTime)
            {
                lootCDTimeCounting = 0;
                lootCD = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, playerScript.transform.position) < 2.5f)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
    }

    IEnumerator waitGive()
    {
        if(!lootCD)
        {
            Debug.Log("dando");
            animator.SetBool("isGiving", true);
            GameObject Loot = Instantiate(loot, hand.transform.position, transform.rotation);
            lootCD = true;
            yield return new WaitForSeconds(1);
            animator.SetBool("isGiving", false);
            Debug.Log("deu");
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsColliding = true;
            Debug.Log("player colidindo");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsColliding = true;
            Debug.Log("player colidindo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsColliding = false;
            Debug.Log("player nao mais colidindo");
        }
    }
}
