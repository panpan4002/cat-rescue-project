using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathEnemy : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D golemRb;
    private enemy enemyScript;
    private SpriteRenderer golemSpriteRenderer;
    private Player playerScript;
    private GameObject player;

    [Header("Movement")]
    public int direction = 1;
    public float enemySpeed;

    [Header("Raycast")]
    private RaycastHit2D rightWall;
    private RaycastHit2D leftWall;
    private RaycastHit2D rightEdge;
    private RaycastHit2D leftEdge;

    public Vector2 wallOffSet;
    public Vector2 edgeOffSet;
    public LayerMask Ground;
    public LayerMask Wall;

    [Header("Stats")]
    public int enemyDamage;
    private bool fate;
    private float alpha = 1;

    [Header("Animation")]
    private Animator animator;

    void Start()
    {
        golemSpriteRenderer = GetComponent<SpriteRenderer>();
        golemRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemyScript = GetComponent<enemy>();
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        //Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>(), true);
    }

    void Update()
    {
        
        if (enemyScript.isDead)
        {
            StartCoroutine("Die");
            Color color = golemSpriteRenderer.color;
            golemSpriteRenderer.color = new Color(color.r, color.g, color.b, alpha);
            if (fate) alpha -= 0.0018f;
        }

        CheckSurroundings();

        if (!enemyScript.isDead)
        {
            if (direction == -1)
            {
                transform.eulerAngles = new Vector2(0f, 180f);
            }
            if (direction == 1)
            {
                transform.eulerAngles = new Vector2(0f, 0);
            }
            enemyPathMovement();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!enemyScript.isDead && col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().playerTakeDamageAUX(true, enemyDamage);
        }
    }

    private void enemyPathMovement()
    {
        golemRb.velocity = new Vector2(enemySpeed * direction, golemRb.velocity.y);
    }

    private void CheckSurroundings()
    {
        rightEdge = Physics2D.Raycast(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 1f, Ground);
        Debug.DrawRay(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, Color.yellow);

        if (rightEdge.collider == null)
        {
            direction = -1;
        }

        leftEdge = Physics2D.Raycast(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 1f, Ground);
        Debug.DrawRay(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, Color.yellow);

        if (leftEdge.collider == null)
        {
            direction = 1;
        }

        rightWall = Physics2D.Raycast(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right, 1f, Wall);
        Debug.DrawRay(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right, Color.red);

        if (rightWall.collider != null)
        {
            direction = -1;
        }

        leftWall = Physics2D.Raycast(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left, 1f, Wall);
        Debug.DrawRay(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left, Color.red);

        if (leftWall.collider != null)
        {
            direction = 1;
        }
    }
    IEnumerator Die()
    {
        animator.SetTrigger("isDying");
        golemRb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        fate = true;
        yield return new WaitForSeconds(8f);

        Destroy(gameObject);
        fate = false;
    }
}
