using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fate : MonoBehaviour
{
    public Player player;
    public SpriteRenderer spriteRenderer;
    public float fateIntensity;
    public GameObject text;
    void Start()
    {
       spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
    void Update()
    {
        text.GetComponent<TextMesh>().color = new Color(1, 1, 1, 1 - (Vector2.Distance(player.transform.position, transform.position)) * fateIntensity);
        spriteRenderer.color = new Color(1, 1, 1, 1 - (Vector2.Distance(player.transform.position, transform.position)) * fateIntensity);
    }
}
