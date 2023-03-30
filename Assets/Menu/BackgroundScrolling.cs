using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public Material background;
    public float speed;

    void Start()
    {

    }

    void Update()
    {
        background.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
