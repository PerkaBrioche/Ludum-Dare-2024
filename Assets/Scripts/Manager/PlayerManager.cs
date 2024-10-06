using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Space(10)]
    [Header("PLAYER PARAMETER")]

    public float PLAYER_CoolDownAttack;
    public float PLAYER_Damage;
    public float PLAYER_Speed;
    public float PLAYER_KnockBackForce = 5f;
    public float PLAYER_GodMode = 0.7f;
    public float PLAYER_Life = 10;

}
