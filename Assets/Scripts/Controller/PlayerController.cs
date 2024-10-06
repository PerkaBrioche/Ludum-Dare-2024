using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("DETAILS KNOCKBACK")]
    private Vector2 knockbackVelocity = Vector2.zero;
    [SerializeField] private float knockbackDuration = 0.5f;
    [SerializeField] private float KnockBackSmoothness = 5f;
    [SerializeField] private float knockbackEndTime;
    [SerializeField] private bool isKnockback = false;

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
    [Space(10)] 
    [Header("OTHER")] 
    public ParticleSystem PART_InkParticule;

    public GameObject OBJ_UserInterface;

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
        if (isKnockback)
        {
            // Si on est en train de subir un knockback, applique la vélocité de knockback
            RB_RigidBody.MovePosition(RB_RigidBody.position + knockbackVelocity * Time.fixedDeltaTime);
        }
        else
        {
            // Mouvement normal quand il n'y a pas de knockback
            Vector2 VectorNormalized = VECTOR2_movement.normalized;

            if (VectorNormalized.magnitude > 0.1f)
            {
                velocity = Vector2.Lerp(velocity, VectorNormalized * PlayerManager.Instance.PLAYER_Speed, acceleration * Time.fixedDeltaTime);
            }
            else
            {
                velocity = Vector2.Lerp(velocity, Vector2.zero, deceleration * Time.fixedDeltaTime);
            }

            RB_RigidBody.MovePosition(RB_RigidBody.position + velocity * Time.fixedDeltaTime);
        }
    }

    public bool IsMoving()
    {
        return inputX != 0 || inputY != 0;
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

        CanBeDamage = false;
        InkManager.Instance.Ink -= ennemyData.Ennemy_Damage;

        if (InkManager.Instance.Ink <= 0)
        {
            Dead();
            return;
        }
        VolumeManger.Instance.PlayAnim(1);

        StartCoroutine(CooldownGod());

        // Calculer la direction du knockback
        Vector2 knockbackDirection = (transform.position - ennemyTransform).normalized; // L'ennemi pousse le joueur
        knockbackVelocity = knockbackDirection * ennemyData.knockbackForce;

        isKnockback = true;
        knockbackEndTime = Time.time + knockbackDuration;

        StartCoroutine(SmoothKnockback());
        ShakeManager.instance.ShakeCamera(0.7f, 0.3f);
    }

    private IEnumerator SmoothKnockback()
    {
        float elapsedTime = 0f;

        while (elapsedTime < knockbackDuration)
        {
            // Lerp entre la vélocité actuelle du knockback et zéro
            knockbackVelocity = Vector2.Lerp(knockbackVelocity, Vector2.zero, elapsedTime / knockbackDuration * KnockBackSmoothness);
            elapsedTime += Time.deltaTime;
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

    public void Dead()
    {
        OBJ_UserInterface.SetActive(false);
        VolumeManger.Instance.PlayAnim(2);
        TimeManager.instance.SlowMotion(0, 1);
    }

    public void GetInk()
    {
        PART_InkParticule.Play();
        VolumeManger.Instance.PlayAnim(0);
        InkManager.Instance.GainInk(Random.Range(2,3));
    }
}
