using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public bool activated = false;
    public Player player;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().Pedestal = true;

            if (player.GetComponent<Player>().hasTheMap == true)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Submit"))
                {
                    activated = true;
                    player.GetComponent<Player>().hasTheMap = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player>().Pedestal = true;

            if (player.GetComponent<Player>().hasTheMap == true)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetButtonDown("Submit"))
                {
                    activated = true;
                    player.GetComponent<Player>().hasTheMap = false;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) player.GetComponent<Player>().Pedestal = false;
    }
}
