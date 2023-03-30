using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossProjectile : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    private int projectileDamage = 1;

    private Vector2 playerPos = new Vector2(0, 0);

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();

        playerPos = playerScript.transform.position;

        Vector3 targ = playerScript.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Update()
    {
        transform.position += transform.right * 10 * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Player>().playerTakeDamageAUX(true, projectileDamage);
        }
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
