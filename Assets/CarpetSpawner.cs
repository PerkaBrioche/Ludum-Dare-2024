using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarpetSpawner : MonoBehaviour
{
    public List<Sprite> ListCarpet;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = ListCarpet[Random.Range(0, ListCarpet.Count)];
        var random = Random.Range(0, 2);
        if(random == 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipY = true;
        }
    }
}
