using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public float ENEMY_KnockBackResistance;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    
}
