using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnnemyController>().Die();
        }

        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Dead();
        }
    }
}
