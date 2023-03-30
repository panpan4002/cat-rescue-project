using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paredeQuebravel : MonoBehaviour
{
    [Header("Atributos")]
    public float maxlife;
    public float life;
    private SpriteRenderer paredeSR;
    private ParticleSystem particulaDano;
    private bool fade;
    private Color color;
    private Projectile projectile;

    void Start()
    {
        particulaDano = GetComponentInChildren<ParticleSystem>();
        paredeSR = GetComponent<SpriteRenderer>();
        color = paredeSR.color;
        life = maxlife; 
    }

    void Update()
    {
        if(fade == true)
        {
            paredeSR.color = color;
            color = new Color(color.r, color.g, color.b, color.a -= Time.deltaTime * 12f);
        }

        if (life <= 0)
        {
            Destroy(GetComponent<PolygonCollider2D>());
            fade = true;
            Invoke("quebrar", 2f);
        }
    }

    public void rachar(float dano)
    {
        life -= dano;
        particulaDano.Play();
        
    }

    void quebrar()
    {
        Destroy(gameObject);
    }
}
