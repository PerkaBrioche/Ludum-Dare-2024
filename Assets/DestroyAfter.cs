using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    public float DestroySec;

    private void Start()
    {
        Destroy(gameObject, DestroySec);
    }
}
