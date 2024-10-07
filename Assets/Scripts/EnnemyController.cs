using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    private float Ennemy_Life;
    private float Ennemy_Damage;
    private float Ennemy_Speed;
    private float knockbackForce ;
    
    private bool CanTakeDamage;
    public float knockbackDuration;
    private Vector2 knockbackVelocity;
    public EnemyAttackController EnemyAttackController;
    public GameObject InkSpawner;
    [Space(10)]
    [Header("KNOCKBACK PARAMETERS")]
    private bool isKnockback = false;
    public float knockbackEndTime; 
    public float KnockBackSmoothness;

    // Ajoute le Rigidbody2D pour gérer le mouvement
    private Rigidbody2D rb;
    
    private void Start()
    {
        EnnemyData EnnemyData = GetComponent<EnemyDataController>().enemyData;
        CanTakeDamage = true;

        Ennemy_Life = EnnemyData.Ennemy_Life;
        Ennemy_Damage = EnnemyData.Ennemy_Damage;
        Ennemy_Speed = EnnemyData.Ennemy_Speed;
        knockbackForce = EnnemyData.knockbackForce;

        // Initialisation du Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        // Appliquer le knockback si en état de knockback
        if (isKnockback)
        {
            rb.MovePosition(rb.position + knockbackVelocity * Time.fixedDeltaTime);
        }
    }

    private IEnumerator WaitDamage()
    {
        yield return new WaitForSeconds(0.4f);
    }
    
    public void GetDamage(Vector2 playerPosition, float DamageToTake)
    {
        EnemyAttackController.ResetTimer();
        ShakeManager.instance.ShakeCamera(0.35f, 0.1f);
        Ennemy_Life -= DamageToTake;
        if (Ennemy_Life <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(WaitDamage());

        // Calculer la direction inverse du joueur
        Vector2 knockbackDirection = (transform.position - (Vector3)playerPosition).normalized;
        knockbackVelocity = knockbackDirection * GetKnockBack();
        isKnockback = true;
        knockbackEndTime = Time.time + knockbackDuration;

        StartCoroutine(SmoothKnockback());
    }
    
    private IEnumerator SmoothKnockback()
    {
        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, elapsedTime / knockbackDuration * KnockBackSmoothness);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isKnockback = false;
        knockbackVelocity = Vector2.zero;
    }

    private float GetKnockBack()
    {
        var Knock = (PlayerManager.Instance.PLAYER_KnockBackForce) - EnemyManager.Instance.ENEMY_KnockBackResistance;
        return Knock;
    }

    public void Die()
    {
        Instantiate(InkSpawner, transform.position, InkSpawner.transform.rotation);
        Destroy(gameObject);
    }
}
