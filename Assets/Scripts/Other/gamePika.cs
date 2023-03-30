using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gamePika : MonoBehaviour
{
    private static gamePika pikaInstance;
    public Vector2 lastCheckPoint;
    public bool checkPointRespawn;

    void Awake()
    {
        if(pikaInstance == null)
        {
            pikaInstance = this;
            DontDestroyOnLoad(pikaInstance);

        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
