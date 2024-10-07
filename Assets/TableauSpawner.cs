using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableauSpawner : MonoBehaviour
{
    public List<Sprite> ListTabbleau;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = ListTabbleau[Random.Range(0, ListTabbleau.Count)];
    }
}
