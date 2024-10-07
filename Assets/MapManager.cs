using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    public int HandSpawnChance = 5;
    public static MapManager Instance;
    public int StageCompleted;
    public TextMeshProUGUI TMP_Chapter;

    public List<GameObject> LIST_MapInstance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateChatper();
    }

    public void NewMap()
    {
        HandTracker.Instance.StopHand();
        StageCompleted++;
        UpdateChatper();
    }

    public void UpdateChatper()
    {
        TMP_Chapter.text = "Chapter " + StageCompleted;
    }

    private void TryingToHand()
    {
        if (Random.Range(0, HandSpawnChance) == 0)
        {
            HandSpawnChance = 5;
            HandTracker.Instance.InitliazeHand();
        }
        else
        {
            HandSpawnChance--;
        }
    }
}
