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
    private bool CheckingEyes;
    
    
    

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
            }
            transform.position += chargeDirection * chargeSpeed * Time.deltaTime;
            CloseEyes();
        }
        ColliderAttack.enabled = isCharging;

        if (CheckingEyes)
        {
            OpenEyes();
        }
    }

    private void OpenEyes()
    {
        if (PlayerController.Instance.IsMoving())
        {
            StopAllCoroutines();
            Attaque();
            CheckingEyes = false;
            return;
        }
        StartCoroutine(StartClosingEyes());
    }
    private void CloseEyes()
    {
        SpriteRendererTest.color = Color.grey;
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
        SpriteRendererTest.color = Color.yellow;
        yield return new WaitForSeconds(0.5f);
        CheckingEyes = true;
    }
    
}
