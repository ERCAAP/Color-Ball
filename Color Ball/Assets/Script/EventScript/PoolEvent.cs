using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEvent : MonoBehaviour
{
    public Animator anim;
  
    public ParticleSystem[] Confecti;


    public void FinishEvent()
    {
        anim.SetTrigger("Play");
        for (int i = 0; i < Confecti.Length; i++)
        {
            Confecti[i].Play();
        }
    }
}
