using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemyLife;
    public float enemyMaxLife;
    public bool isDead;
    public GameObject floatingDamage;

    private void Start()
    {
        enemyLife = enemyMaxLife;
        isDead = false;
    }
    private void Update()
    {
        if (enemyLife <= 0) isDead = true;
    }

    public void enemyTakeDamage(float damage)
    {
        if (!isDead)
        {
            enemyLife -= damage;
            GameObject floatingDamageInstantiated = Instantiate(floatingDamage, transform.position, Quaternion.identity);
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().text = "-" + damage.ToString();
            floatingDamageInstantiated.GetComponentInChildren<TextMesh>().color = Color.white;
            GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
            StartCoroutine(redDamage());
        }
    }

    IEnumerator redDamage()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().color = new Color(1, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
}
