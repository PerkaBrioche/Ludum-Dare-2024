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
        EnnemyData = transform.parent.transform.parent.GetComponent<EnnemyController>().EnnemyData;
        ResetTimer();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerController == null)
            {
                PlayerController = other.GetComponent<PlayerController>();
            }
            ReduceTimer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ResetTimer();
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
    }
    private void DamagePlayer()
    {
        PlayerController.GetDamage(EnnemyData, transform.transform.position);
    }
}