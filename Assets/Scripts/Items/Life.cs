using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public int regenAmount;

    private GameObject player;
    private Player playerScript;
    public GameObject floatingDamage;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameObject floatingDamageInstantiated = Instantiate(floatingDamage, transform.position, Quaternion.identity);
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().text = "+" + regenAmount.ToString();
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().color = Color.green;
            playerScript.playerLife = playerScript.playerLife + regenAmount;
            Destroy(gameObject);
        }
    }
}
