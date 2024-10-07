using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataController : MonoBehaviour
{
   public EnnemyData enemyData;

    public bool Fantassin;

    private void Start()
    {
        if (Fantassin)
        {
            enemyData = ScaleManger.Instance.GetFantassinData();
        }
        else
        {
            enemyData = ScaleManger.Instance.GetEyesData();
        }
    }
}
