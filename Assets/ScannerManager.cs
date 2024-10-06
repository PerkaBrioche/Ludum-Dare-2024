using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ScannerManager : MonoBehaviour
{
    public static ScannerManager Instance;

    private void Start()
    {
        Scan();
    }

    public void Scan()
    {
        AstarPath.active.Scan();
    }
}
