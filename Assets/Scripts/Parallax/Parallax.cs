using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject CameraPlayer;

    private float sizeX,sizeY, startPosX, startPosY;
    public float parallaxSpeed;

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        sizeX = GetComponent<SpriteRenderer>().bounds.size.x;
        sizeY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        // x
        float tempX = (CameraPlayer.transform.position.x * (1 - parallaxSpeed));
        float distX = (CameraPlayer.transform.position.x * parallaxSpeed);

        transform.position = new Vector2(startPosX + distX, transform.position.y);

        if(tempX > startPosX + sizeX /2)
        {
            startPosX += sizeX;
        }
        else if(tempX < startPosX - sizeX /2)
        {           
            startPosX -= sizeX;
        }

        // y

        float tempY = (CameraPlayer.transform.position.y * (1 - parallaxSpeed));
        float distY = (CameraPlayer.transform.position.y * parallaxSpeed);

        transform.position = new Vector2(transform.position.x, startPosY + distY / 2);

        if (tempY > startPosY + sizeY / 2)
        {
            startPosY += sizeY;
        }
        else if (tempY < startPosY - sizeY / 2)
        {
            startPosY -= sizeY;
        }
    }
}
