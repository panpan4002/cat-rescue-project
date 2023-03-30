using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;

public class checkPoint : MonoBehaviour
{
    private Vector2 checkPointLocation;
    private Player player;
    private gamePika gamePikaFoda;

    public AudioSource checkPointSource;

    void Start()
    {
        gamePikaFoda = GameObject.FindGameObjectWithTag("gamePika").GetComponent<gamePika>();
        checkPointLocation = new Vector2(transform.position.x, transform.position.y - 1);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
           player = collision.gameObject.GetComponent<Player>();
           if(player.restartCheckPointLocation != checkPointLocation)
           {
                checkPointSource.Play();
                GetComponent<Light2D>().intensity = .5f;
                //player.updateCheckPoint(checkPointLocation);
                gamePikaFoda.lastCheckPoint = checkPointLocation;
           }
        }
    }
}
