using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hypershoot : MonoBehaviour
{
    public float damage;
    public GameObject player;
    public Player playerScript;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        damage = playerScript.weaponDamage * 2;
        Invoke("DestroyProjectile", 0.3f);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<enemy>().enemyTakeDamage(damage);
            Debug.Log("bateu");
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
