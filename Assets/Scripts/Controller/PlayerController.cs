using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("DETAILS KNOCKBACK")]
    private Vector2 knockbackVelocity = Vector2.zero;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float KnockBackSmoothness;
    [SerializeField] private float knockbackEndTime;
    [SerializeField] private bool isKnockback;
    [Space(20)]

    public static PlayerController Instance;

    private bool CanBeDamage;
    private Rigidbody2D RB_RigidBody;
    
    [Space(10)]
    public Vector2 VECTOR2_movement;
    [Space(10)]
    [Header("SMOOTH DEPLACEMENT")]
    
    public float acceleration = 10f;
    public float deceleration = 10f;
    private Vector2 velocity = Vector2.zero;

    private float inputX;
    private float inputY;
    [Space(10)] [Header("OTHER")] public ParticleSystem PART_InkParticule;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        RB_RigidBody = GetComponent<Rigidbody2D>();
        CanBeDamage = true;
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        
        VECTOR2_movement = new Vector2(inputX, inputY);
    }

    void FixedUpdate()
    {
        Vector2 VectorNormalized = VECTOR2_movement.normalized;

        if (VectorNormalized.magnitude > 0.1f)
        {
            velocity = Vector2.Lerp(velocity, VectorNormalized * PlayerManager.Instance.PLAYER_Speed, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            velocity = Vector2.Lerp(velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
        }
        RB_RigidBody.MovePosition(RB_RigidBody.position + knockbackVelocity + velocity * Time.fixedDeltaTime);    
    }

    public bool IsMoving()
    {
        if (inputX != 0 || inputY != 0)
        {
            return true;
        }
        return false;
    }
    
    public Vector2 GetFacingDirection()
    {
        if (inputX != 0 || inputY != 0)
        {
            return new Vector2(inputX, inputY).normalized;
        }
        return Vector2.right;
    }

    public void GetDamage(EnnemyData ennemyData, Vector3 ennemyTransform)
    {
        if (!CanBeDamage)
        {
            return;
        }
        print("APPLY DAMAGE");
        CanBeDamage = false;
        PlayerManager.Instance.PLAYER_Life -= ennemyData.Ennemy_Damage;
        if (PlayerManager.Instance.PLAYER_Life <= 0)
        {
            Dead();
            return;
        }
        
        StartCoroutine(CooldownGod());
        
        Vector2 knockbackDirection = (transform.position - (Vector3)ennemyTransform).normalized;
        print("knockbackDirection " + knockbackDirection);
        knockbackVelocity = knockbackDirection * ennemyData.knockbackForce;
        print("knockbackVelocity " + knockbackVelocity);

        isKnockback = true;
        knockbackEndTime = Time.time + knockbackDuration;
        StartCoroutine(SmoothKnockback());
        ShakeManager.instance.ShakeCamera(0.7f, 0.2f);
        TimeManager.instance.SetTimeScale(0.5f);
        TimeManager.instance.SlowMotion(1f, 0.7f);

    }
    
    private IEnumerator SmoothKnockback()
    {
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, elapsedTime / knockbackDuration * KnockBackSmoothness);
            elapsedTime += Time.deltaTime;
            print("knockbackVelocity Live " + knockbackVelocity);

            yield return null;
        }
        isKnockback = false;
        knockbackVelocity = Vector2.zero;
    }

    private IEnumerator CooldownGod()
    {
        yield return new WaitForSeconds(PlayerManager.Instance.PLAYER_GodMode);
        CanBeDamage = true;
    }

    private void Dead()
    {
    }

    public void GetInk()
    {
        PART_InkParticule.Play();
        VolumeManger.Instance.PlayAnim(0);
    }

    
    
}