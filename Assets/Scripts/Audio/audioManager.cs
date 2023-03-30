using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class audioManager : MonoBehaviour
{
    public sound[] sounds;
    public static audioManager instance;
    public AudioMixerGroup audioMixer;

    private void Awake()
    {
        /*if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);*/

        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixer;
            s.source.spatialBlend = s.is3D;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
        }
    }

    void Start()
    {
        playSound("CaveSound");
        playSound("D1 Theme");
    }

    void Update()
    {
        
    }

    public void playSound(string soundName)
    {
        sound s = Array.Find(sounds, sound => sound.name == soundName);
        if(s == null)
        {
            Debug.LogWarning("Audio wasn't found");
            return;
        }

        s.source.Play();
    }

    public void pauseSound(string soundName)
    {
        sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Audio wasn't found");
            return;
        }

        s.source.Pause();
    }
    public void resumeSound(string soundName)
    {
        sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Audio wasn't found");
            return;
        }

        s.source.UnPause();
    }

    public void stopSound(string soundName)
    {
        sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("Audio wasn't found");
            return;
        }

        s.source.Stop();
    }
}
