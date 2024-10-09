using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class StoreDataPlayer : MonoBehaviour
{
    public static StoreDataPlayer Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public string PlayerName; 
    public void StoreData(string PlayerNames)
    {
        PlayerName = PlayerNames;
    }
}
