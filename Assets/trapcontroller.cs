using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class trapcontroller : MonoBehaviour
{
    private bool CanFall;
    public List<Sprite> LIST_Sprite;

    private void Start()
    {
        CanFall = true;
        GetComponent<SpriteRenderer>().sprite = LIST_Sprite[Random.Range(0,LIST_Sprite.Count)];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBool")
        {
            if (!CanFall) { return;}
            CanFall = false;
            BallManager.Instance.BallFall(transform);
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
