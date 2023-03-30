using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public Player player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        if(player.fall) player.fall = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            player.isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            if (Time.time > .2f)
            {
                player.fall = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            player.isGrounded = false;
        }
    }
}
