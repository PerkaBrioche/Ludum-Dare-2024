using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Coroutinehand());
    }

    private IEnumerator Coroutinehand()
    {

        yield return new WaitForSeconds(1.55f);
        SoundManager.Instance.PlaySoundpoing(0);
        ShakeManager.instance.ShakeCamera(1.3f,0.6f);
        yield return new WaitForSeconds(3.4f);
        Destroy(gameObject);
    }
}
