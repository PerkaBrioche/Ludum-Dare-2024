using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    public List<GameObject> LIST_MapInstance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
