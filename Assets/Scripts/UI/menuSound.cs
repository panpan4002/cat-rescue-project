using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSound : MonoBehaviour
{
    public AudioSource menuSoundEffect;
    public AudioClip menuHoverEffect;
    public AudioClip menuClickEffect;
    public AudioClip menuBackEffect;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void menuHoverSound()
    {
        menuSoundEffect.PlayOneShot(menuHoverEffect);
    }

    public void menuClickSoud()
    {
        menuSoundEffect.PlayOneShot(menuClickEffect);
    }

    public void menuBackSound()
    {
        menuSoundEffect.PlayOneShot(menuBackEffect);
    }
}
