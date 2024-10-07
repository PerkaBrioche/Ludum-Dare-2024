using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManager : MonoBehaviour
{
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
        StageCompleted++;

        UpdateChatper();
    }

    public void UpdateChatper()
    {
        TMP_Chapter.text = "Chapter " + StageCompleted;
    }
}
