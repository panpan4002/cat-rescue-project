using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileDamage;
    private float projectileDespawn = 1.5f;
    public bool isColliding = false;
    private GameObject player;
    private Player playerScript;
    private Rigidbody2D projectileRB;

    private Animator projectileAnimator;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        projectileDamage = playerScript.weaponDamage;
        projectileAnimator = GetComponent<Animator>();
        projectileRB = GetComponent<Rigidbody2D>();

        Invoke("DestroyProjectile", projectileDespawn);
    }

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemy enemyCol = collision.GetComponent<enemy>();

            if (!enemyCol.isDead)
            {
                enemyCol.enemyTakeDamage(projectileDamage);
                Debug.Log("bateu");
                DestroyProjectile();
            }
        }

        else if (collision.CompareTag("paredeQuebravel"))
        {
            paredeQuebravel paredeCol = collision.gameObject.GetComponent<paredeQuebravel>();
            paredeCol.rachar(projectileDamage);
            projectileAnimator.SetTrigger("splashTrigger");
            projectileRB.velocity = new Vector2(0, 0);
            Invoke("DestroyProjectile", .2f);

        }

        else if (collision.CompareTag("elementoFundo"))
        {

        }

        else if (collision != null)
        {
            projectileAnimator.SetTrigger("splashTrigger");
            projectileRB.velocity = new Vector2(0, 0);
            Invoke("DestroyProjectile", .2f);
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
