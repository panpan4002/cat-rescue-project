using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal2 : MonoBehaviour
{
    public Pedestal pedestal;
    public Animator animator;

    void Start()
    {
        
    }

    void Update()
    {
        if (pedestal.activated == true) this.animator.SetBool("isActivated", true);
    }
}
