using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBallImage : MonoBehaviour
{
    public List<GameObject> LIST_Life;


    public void UpdateLife(int Life)
    {
        var life = Life;
        for (int i = 0; i < LIST_Life.Count; i++)
        {
            if (life > 0)
            {
                LIST_Life[i].SetActive(true);
            }
            else
            {
                LIST_Life[i].SetActive(false);
            }
            life--;
        }
    }
}
