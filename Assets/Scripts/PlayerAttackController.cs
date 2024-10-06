using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField] private List<EnnemyController> List_EnnemyController;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (AlreadyTouched(other))
            {
                return;
            }

            List_EnnemyController.Add(other.GetComponent<EnnemyController>());
            other.GetComponent<EnnemyController>().GetDamage(transform.position , PlayerManager.Instance.PLAYER_Damage);
            StartCoroutine(ResetController());
        }
    }


    private IEnumerator ResetController()
    {
        yield return new WaitForSeconds(0.5f);
        List_EnnemyController.Clear();
    }

    private bool AlreadyTouched(Collider2D Hit)
    {
        if (List_EnnemyController.Count == 0)
        {
            return false;
        }
        foreach (var Controller in List_EnnemyController)
        {
            if (Controller == Hit.GetComponent<EnnemyController>())
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
