using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giantMushroom : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Collision");
        }
    }
}
