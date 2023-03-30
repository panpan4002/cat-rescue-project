using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalFlyLaser : MonoBehaviour
{
    private cristalFly vitrum;
    public PolygonCollider2D laserCol1;
    private SpriteRenderer laserSR;
    public LayerMask tileMapGround, tileMapWall;
    public bool isHittingSomething, isHittingPlayer;

    void Start()
    {
        vitrum = GetComponentInParent<cristalFly>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (laserCol1.enabled == true)
        {
           if(collision != null)
            {
                if (collision.CompareTag("Player"))
                {
                    isHittingPlayer = true;
                }

                else if (collision.CompareTag("Ground") || collision.CompareTag("Wall")) ;
                {
                    isHittingSomething = true;
                    vitrum.particulaLaser1.transform.position = collision.transform.position;
                    vitrum.particulaLaser1.Play();
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (laserCol1.enabled == true)
        {
            if (collision != null)
            {
                if (collision.CompareTag("Player"))
                {
                    isHittingPlayer = true;
                }

                else if (collision.CompareTag("Ground") || collision.CompareTag("Wall"));
                {
                    isHittingSomething = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (laserCol1.enabled == true)
        {
            if (collision != null)
            {
                if (collision.CompareTag("Player"))
                {
                    isHittingPlayer = false;
                }

                else if (collision.CompareTag("Ground") || collision.CompareTag("Wall")) ;
                {
                    isHittingSomething = false;
                }
            }
        }
    }
}
