using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("MAP PARAMETERS")]
    public List<EnnemySpawner> List_EnemySpawner;
    public List<TrapSpawner> List_TrapSpawner;
    public List<GameObject> List_Decorations;
    public PlayerSpawner PlayerSpawner;
    public GameObject ExitDoor;
    public float CameraValue;
}
