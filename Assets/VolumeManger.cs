using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManger : MonoBehaviour
{
    public static VolumeManger Instance;
    private bool Lockl;

    public List<string> LIST_AnimString;
    private Animator Anim;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        Anim = GetComponent<Animator>();
    }

    public void PlayAnim(int Index, bool locked = false)
    {
        if (Lockl) { return;}
        Lockl = locked;
        Anim.Play(LIST_AnimString[Index], -1, 0f);
    }

}
