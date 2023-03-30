using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingSpike : MonoBehaviour
{
    private RaycastHit2D raycast;
    public float distance;
    public bool isBelow;
    public bool infestantibusSpike;
    public Rigidbody2D rb;
    public float offset = 2;
    private SpriteRenderer fallingSpikeSR;
    public Sprite[] sprites;

    void Start()
    {
        fallingSpikeSR = GetComponent<SpriteRenderer>();
        fallingSpikeSR.sprite = sprites[Mathf.RoundToInt(Random.Range(1, sprites.Length))];
    }

    void Update()
    {
        if(infestantibusSpike)
        {
            Invoke("DestroySpike", 2f);
        }

        else
        {
            CheckPlayer();
        }
    }

    void CheckPlayer()
    {
        raycast = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + -offset), Vector2.down * distance);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + -offset), Vector2.down * distance, Color.yellow);

        if (raycast.transform != null)
        {
            if(raycast.transform.tag == "Player")
            {
                isBelow = true;
                rb.gravityScale = 5;
                Invoke("DestroySpike", 2f);
            }
            isBelow = false;
        }
    }

    void DestroySpike()
    {
        Destroy(gameObject);    
        }
}
