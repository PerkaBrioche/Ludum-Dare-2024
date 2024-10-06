using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private float TimerAttack;
    private EnnemyData EnnemyData;
    private PlayerController PlayerController;

    private void Awake()
    {
        EnnemyData = transform.parent.transform.parent.GetComponent<EnemyDataController>().enemyData;
        ResetTimer();
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "PlayerBool")
        {
            ReduceTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "PlayerBool")
        {
            ResetTimer();
        }
    }

    private void ReduceTimer()
    {
        if (TimerAttack > 0)
        {
            TimerAttack -= Time.deltaTime;
        }
        else
        {
            DamagePlayer();
            ResetTimer();
        }
    }
    

    public void ResetTimer()
    {
        TimerAttack = EnnemyData.Ennemy_AttackWindow;
        Debug.LogError("TimerAttack = "+ TimerAttack);
    }
    private void DamagePlayer()
    {
        PlayerController.GetDamage(EnnemyData, transform.transform.position);
    }
}