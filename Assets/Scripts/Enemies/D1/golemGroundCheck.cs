using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golemGroundCheck : MonoBehaviour
{
    public BoxCollider2D golemGroundCheckCollider;
    public bool fall;
    public bool isGrounded;
    void Start()
    {
        golemGroundCheckCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        fall = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(Time.time > .1f)
            {
                fall = true;
            }

            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
