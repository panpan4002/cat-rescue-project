using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundElementWalking : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D elementRB;

    [Header("Movement")]
    public int direction;
    public float elementSpeed;

    [Header("Raycast")]
    private RaycastHit2D rightWall;
    private RaycastHit2D leftWall;
    private RaycastHit2D rightEdge;
    private RaycastHit2D leftEdge;

    public Vector2 wallOffSet;
    public Vector2 edgeOffSet;
    public LayerMask Ground;
    public LayerMask Wall;

    void Start()
    {
        elementRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        CheckSurroundings();
    }

    void Movement()
    {
        elementRB.velocity = new Vector2(direction * elementSpeed, 0);
    }
    private void CheckSurroundings()
    {
        rightEdge = Physics2D.Raycast(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 1f, Ground);
        Debug.DrawRay(new Vector2(transform.position.x + edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, Color.yellow);

        if (rightEdge.collider == null)
        {
            direction = -1;
            transform.eulerAngles = new Vector2(0f, 0);
        }

        leftEdge = Physics2D.Raycast(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, 1f, Ground);
        Debug.DrawRay(new Vector2(transform.position.x - edgeOffSet.x, transform.position.y + edgeOffSet.y), Vector2.down, Color.yellow);

        if (leftEdge.collider == null)
        {
            direction = 1;
            transform.eulerAngles = new Vector2(0f, 180);
        }

        rightWall = Physics2D.Raycast(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right, 1f, Wall);
        Debug.DrawRay(new Vector2(transform.position.x + wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.right, Color.red);

        if (rightWall.collider != null)
        {
            direction = -1;
            transform.eulerAngles = new Vector2(0f, 0);
        }

        leftWall = Physics2D.Raycast(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left, 1f, Wall);
        Debug.DrawRay(new Vector2(transform.position.x - wallOffSet.x, transform.position.y + wallOffSet.y), Vector2.left, Color.red);

        if (leftWall.collider != null)
        {
            direction = 1;
            transform.eulerAngles = new Vector2(0f, 180);
        }
    }
}
