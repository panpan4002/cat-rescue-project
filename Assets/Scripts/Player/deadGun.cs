using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadGun : MonoBehaviour
{
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Player playerScript = player.GetComponent<Player>();
        Physics2D.IgnoreCollision(playerScript.GetComponent<CapsuleCollider2D>(), GetComponent<CapsuleCollider2D>(), true);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-2, 1), ForceMode2D.Impulse);
    }
    void Update()
    {
        
    }
}
