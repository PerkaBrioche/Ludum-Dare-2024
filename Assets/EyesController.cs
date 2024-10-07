using System.Collections;
using UnityEngine;

public class EyesController : MonoBehaviour
{
    public float TimeEyesOpen;
    public float TimeEyesClose;
    public float chargeSpeed = 10f;
    public float decelerationSpeed = 5f; 
    public float stopDistance = 1f;
    private Vector3 targetPosition;
    private bool isCharging = false;
    private bool isDecelerating = false;
    private Vector3 chargeDirection;
    private float initialSpeed;

    private Transform playerTransform;

    public SpriteRenderer SpriteRendererTest;
    public BoxCollider2D ColliderAttack;
    public SpriteController SpriteController;
    private bool CheckingEyes;

    private bool LaunchClosingEyes;
    
    
    

    void Start()
    {
        chargeSpeed = GetComponent<EnemyDataController>().enemyData.Ennemy_Speed;
        initialSpeed = chargeSpeed; 
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        CloseEyes();
    }

    void Update()
    {
        if (isCharging)
        {
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, targetPosition) <= stopDistance)
            {
                isCharging = false;
                isDecelerating = true;
            }
        }

        if (isDecelerating)
        {
            chargeSpeed -= decelerationSpeed * Time.deltaTime;
            if (chargeSpeed <= 0.1f)
            {
                chargeSpeed = 0f;
                isDecelerating = false;
                CloseEyes();
            }
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
        }
        ColliderAttack.enabled = isCharging;
        if (CheckingEyes)
        {
            if (PlayerController.Instance.IsMoving())
            {
                SoundManager.Instance.PlaySoundOeil(1);
                Attaque();
                CheckingEyes = false;
                return;
            }

            if (LaunchClosingEyes)
            {
                LaunchClosingEyes = false;
                StartCoroutine(StartClosingEyes());
            }
        }
    }
    
    private void CloseEyes()
    {
        SpriteController.UpdateSprite(0);
        StartCoroutine(StartOpeningEyes());
    }

    public void Attaque()
    {
        targetPosition = playerTransform.position;
        chargeDirection = (targetPosition - transform.position).normalized;
        chargeSpeed = initialSpeed;
        isCharging = true;
        isDecelerating = false;
    }

    private IEnumerator StartClosingEyes()
    {
        yield return new WaitForSeconds(TimeEyesOpen);
        CloseEyes();
        CheckingEyes = false;
    }
    private IEnumerator StartOpeningEyes()
    { 
        yield return new WaitForSeconds(TimeEyesClose);
        SpriteController.UpdateSprite(1);
        yield return new WaitForSeconds(0.5f);
        SpriteController.UpdateSprite(2);
        CheckingEyes = true;
        LaunchClosingEyes = true;
    }
    
}
