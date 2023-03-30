using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformAtoB : MonoBehaviour
{
    public Player player;
    public float platformSpeed;
    public Transform A;
    public Transform B;
    private bool moveRight = true;
    private bool moveDown = true;
    public bool horizontal;

    void Start()
    {
            
    }

    void Update()
    {
        if (horizontal)
        {
            if (transform.position.x <= A.position.x)
            {
                moveRight = true;
            }

            if (transform.position.x >= B.position.x)
            {
                moveRight = false;
            }

            if (moveRight == true)
            {
                transform.position = new Vector2(transform.position.x + Time.deltaTime * platformSpeed, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - Time.deltaTime * platformSpeed, transform.position.y);
            }
        }
        else
        {
            if (transform.position.y > A.position.y)
            {
                moveDown = true;
            }

            if (transform.position.y < B.position.y)
            {
                moveDown = false;
            }

            if (moveDown == true)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - Time.deltaTime * platformSpeed);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + Time.deltaTime * platformSpeed);
            }
        }
    }
}
