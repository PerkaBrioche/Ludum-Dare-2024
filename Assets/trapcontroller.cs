using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapcontroller : MonoBehaviour
{
    private bool CanFall;

    private void Start()
    {
        CanFall = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBool")
        {
            if (!CanFall) { return;}
            CanFall = false;
            BallManager.Instance.BallFall();
            InkManager.Instance.ReducePalier(true);
            StartCoroutine(RecupFall());
        }
    }

    private IEnumerator RecupFall()
    {
        yield return new WaitForSeconds(1f);
        CanFall = true;
    }
}
