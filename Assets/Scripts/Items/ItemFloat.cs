using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloat : MonoBehaviour
{
    public float itemSpeed;
    private Rigidbody2D itemRB;
    private GameObject player;
    private Player playerScript;
    private float speed1, speed2;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        itemRB =  GetComponent<Rigidbody2D>();
        speed1 = Random.Range(-.08f, .08f);
        speed2 = Random.Range(-.08f, .08f);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, playerScript.transform.position) < 2.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerScript.transform.position, itemSpeed * Time.deltaTime);
        }
        else
        {
            itemRB.velocity = new Vector2(speed1, speed2);
        }
    }
}
