using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrapSpawner : MonoBehaviour
{
    [Header("TRAP PARAMETERS")]
    public int TrapSpawnChanceMax;

    [Space(15)]
    public GameObject OBJ_Trap;

    private void Start()
    {
        if (Random.Range(1, TrapSpawnChanceMax+1) == 1)
        {
            Instantiate(OBJ_Trap, transform.position, OBJ_Trap.transform.rotation);
        }
    }
}
