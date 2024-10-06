using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnnemySpawner : MonoBehaviour
{
    [Header("ENNEMY SPAWN PARAMETERS")] 
    public int INT_Fantassin;
    public int INT_Eyes;
    
    [Space(15)]
    [Header("TOUCHEZ PAS SVP")] 
    [SerializeField] private List<GameObject> EnnemyPrefabs;
    [SerializeField] public List<GameObject> EnnemyList;




    private void Start()
    {
        SpawnEnnemy();
    }

    public void SpawnEnnemy()
    {
        for (int i = 0; i < INT_Fantassin; i++)
        {
            EnnemyList.Add(EnnemyPrefabs[0]);
        }
        for (int i = 0; i < INT_Eyes; i++)
        {
            EnnemyList.Add(EnnemyPrefabs[1]);
        }

        var RANDOM = Random.Range(0, EnnemyList.Count);
        Instantiate(EnnemyList[RANDOM], transform.position, EnnemyList[RANDOM].transform.rotation);
    }

}
