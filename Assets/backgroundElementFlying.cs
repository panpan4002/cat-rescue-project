using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundElementFlying : MonoBehaviour
{
    float direction1, direction2;
    public float elementSpeed;
    private Rigidbody2D elementRB;

    void Start()
    {
        elementRB = GetComponent<Rigidbody2D>();
        direction1 = Random.Range(-.1f, 1f);
        direction2 = Random.Range(-.1f, 1f);
    }

    void Update()
    {
        elementRB.velocity = new Vector2(direction1 * elementSpeed, direction2 * elementSpeed);
        if(direction1 > 0) transform.eulerAngles = new Vector2(0f, 0);
        if(direction1 < 0) transform.eulerAngles = new Vector2(0f, 180);
    }
}
