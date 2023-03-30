using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundFly : MonoBehaviour
{
    private float random1, random2;
    public Vector3 startPos, destination;
    public bool headingStart, headingDestination;

    void Start()
    {
        random1 = Random.Range(-2f, 2f);
        random2 = Random.Range(-2f, 2f);
        startPos = transform.position;
        destination = new Vector3(startPos.x + random1, startPos.y + random2);
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if(transform.position == destination)
        {
            headingStart = true;
            headingDestination = false;
        }

        if(headingStart == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos,  .5f * Time.deltaTime);
        }

        if(headingDestination == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, .5f * Time.deltaTime);
        }

        if(transform.position == startPos)
        {
            random1 = Random.Range(-2f, 2f);
            random2 = Random.Range(-2f, 2f);
            destination = new Vector3(startPos.x + random1, startPos.y + random2);

            headingDestination = true;
            headingStart = false;
        }
    }
}
