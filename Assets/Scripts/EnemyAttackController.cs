using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private float TimerAttack;
    private EnnemyData EnnemyData;
    private PlayerController PlayerController;

    public Animator SoliderAnim;

    private bool PlayerHere;

    private void Start()
    {
        EnnemyData = transform.parent.transform.parent.GetComponent<EnemyDataController>().enemyData;
        ResetTimer();
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (PlayerHere)
        {
            ReduceTimer();
        }
        else
        {
            ResetTimer();
            if(SoliderAnim == null){return;}
            SoliderAnim.SetBool("LoadingAttack", false);
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "PlayerBool")
        {
            PlayerHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" || other.tag == "PlayerBool")
        {
            PlayerHere = false;
        }
    }

    private void ReduceTimer()
    {
        if (TimerAttack > 0)
        {
            TimerAttack -= Time.deltaTime;
            
            if(SoliderAnim == null){return;}
            SoliderAnim.SetBool("LoadingAttack", true);
        }
        else
        {
            DamagePlayer();
            ResetTimer();
            
            if(SoliderAnim == null){return;}
            SoliderAnim.SetBool("LoadingAttack", false);
        }
    }
    

    public void ResetTimer()
    {
        TimerAttack = EnnemyData.Ennemy_AttackWindow;
    }
    private void DamagePlayer()
    {
        PlayerController.GetDamage(EnnemyData, transform.transform.position);
        if (SoliderAnim == null)
        {
            SoundManager.Instance.PlaySoundOeil(0);
        }
        else
        {
            SoliderAnim.SetTrigger("Attack");
            SoundManager.Instance.PlaySoundFantassin(0);
        }
    }
}