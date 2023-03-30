using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    public bool loop;
    [Range (0f, 1f)]
    public int is3D;
    public float minDistance;
    public float maxDistance;

    [HideInInspector]
    public AudioSource source;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
