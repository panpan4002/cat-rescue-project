using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giantSpike : MonoBehaviour
{
    public Player player;
    public float SpikeSpeed;
    public Transform A;

    void Start()
    {
        
    }

    void Update()
    {
        if(player.GetComponent<Player>().hasTheMap == true && transform.position.x > A.transform.position.x)
        {
            transform.position = new Vector2(transform.position.x - Time.deltaTime * SpikeSpeed, transform.position.y);
        }
    }
}
