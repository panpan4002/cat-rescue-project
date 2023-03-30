using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cristalAreaCollider : MonoBehaviour
{
    public bool playerColliding;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerColliding = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerColliding = false;
        }
    }
}
