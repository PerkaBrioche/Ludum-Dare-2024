using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject OBJ_Ink;
    public List<Transform> LIST_Transform;

    private void Start()
    {
        for(int i = 0; i < Random.Range(1,3); i++)
        {
            InstanceObjInk();
        }
        Destroy(gameObject, 1);
    }

    private void InstanceObjInk()
    {
        var Ink = Instantiate(OBJ_Ink, transform.position, OBJ_Ink.transform.rotation);
        int Randoms = Random.Range(0, LIST_Transform.Count);
        Ink.GetComponent<InkController>().TRA_Space = LIST_Transform[Randoms];
        LIST_Transform.RemoveAt(Randoms);
    }
}
