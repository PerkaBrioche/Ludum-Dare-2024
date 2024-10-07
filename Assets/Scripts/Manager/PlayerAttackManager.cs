using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private GameObject OBJ_Rotate;
    [SerializeField] private BoxCollider2D COLLIDER2D_TriggerAttack;
    private GetFacing GetFacing;
    public GameObject OBJ_Attack;
    public Animator ANIM_PlayerAnim;
    
    /// <summary>
    /// ////////////////
    /// </summary>

    [Space(10)] 
    [SerializeField] private bool BOOL_CanAttack;
    
    
    [Space(20)]
    public List<float> LIST_RotationList;

    
    private void Awake()
    {
        GetFacing = FindObjectOfType<GetFacing>();
    }

    private void Update()
    {
        if (PlayerController.Instance.IsMoving())
        {
            ApplyRotation();
        }

        if (Input.GetMouseButtonDown(0) && BOOL_CanAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void ApplyRotation()
    {
        if (GetFacing.ReturnFaceIndex() != 8)
        {
            var ActualRotation = OBJ_Rotate.transform.rotation;
            OBJ_Rotate.transform.rotation = Quaternion.Euler(ActualRotation.x, ActualRotation.y, LIST_RotationList[GetFacing.ReturnFaceIndex()]);
            ANIM_PlayerAnim.SetInteger("AnimState", GetFacing.ReturnFaceIndex());
        }

    }

    private IEnumerator Attack()
    {
        SoundManager.Instance.PlaySoundEncre(Random.Range(0,2));
        StartCoroutine(GetCooldown());
        var Attack = Instantiate(OBJ_Attack, transform.position, OBJ_Rotate.transform.rotation, OBJ_Rotate.transform);
        Destroy(Attack, 0.7f);
        COLLIDER2D_TriggerAttack.enabled = true;
        yield return new WaitForSeconds(0.2f);
        COLLIDER2D_TriggerAttack.enabled = false;
    }
    private IEnumerator GetCooldown()
    {
        BOOL_CanAttack = false;
        yield return new WaitForSeconds(PlayerManager.Instance.PLAYER_CoolDownAttack);
        BOOL_CanAttack = true;
    }
    
    
    
    
}
