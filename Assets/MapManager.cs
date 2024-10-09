using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using Pathfinding; // Pour utiliser AstarPath

public class MapManager : MonoBehaviour
{
    public Animator Anim_Page;
    public Transform MapSpawnerTransform;
    public int HandSpawnChance = 5;
    public static MapManager Instance;
    public int StageCompleted;
    public TextMeshProUGUI TMP_Chapter;
    public TextMeshProUGUI TmpDeathTEXT;

    public List<GameObject> LIST_MapInstance;
    public GameObject OldMap;
    public GameObject Player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        UpdateChapter();
        NewMap();
    }

    public void EndStage()
    {
        HandTracker.Instance.StopHand();
        ScaleManger.Instance.UpdateScale();
        TryingToHand();
        StageCompleted++;
        UpdateChapter();
        Destroy(OldMap);
        StartCoroutine(TurnPage());
    }

    private void NewMap()
    {
        if (OldMap != null)
        {
            DestroyInfo(OldMap.GetComponent<GridManager>());
        }

        var RandomMap = Random.Range(0, LIST_MapInstance.Count);
        OldMap = Instantiate(LIST_MapInstance[RandomMap], LIST_MapInstance[RandomMap].transform.position, Quaternion.Euler(0, 0, 0), MapSpawnerTransform);
        InitializeMapsParameters();
        StartCoroutine(ScanPathfindingGrid());
    }

    private void InitializeMapsParameters()
    {
        Transform Pos = OldMap.GetComponent<GridManager>().PlayerSpawner.transform;
        Player.transform.position = Pos.position;
        for (int i = 0; i < BallManager.Instance.LIST_Ball.Count; i++)
        {
            BallManager.Instance.LIST_Ball[i].transform.position = Pos.position;
        }
    }

    public void UpdateChapter()
    {
        TMP_Chapter.text = "Chapter " + StageCompleted;
        TmpDeathTEXT.text = "You've read " + StageCompleted + " Chapters";
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

    private IEnumerator ScanPathfindingGrid()
    {
        yield return new WaitForEndOfFrame();
        AstarPath.active.Scan();
    }
    
    private IEnumerator TurnPage()
    {
        SoundManager.Instance.PlaySoundpoing(1);
        Anim_Page.Play("PageTurn");
        yield return new WaitForSeconds(0.25f);
        NewMap();
    }

    private void DestroyInfo(GridManager gridManager)
    {
        for (int i = 0; i < gridManager.List_TrapSpawner.Count; i++)
        {
            Destroy(gridManager.List_TrapSpawner[i]);
        }
        for (int i = 0; i < gridManager.List_EnemySpawner.Count; i++)
        {
            Destroy(gridManager.List_EnemySpawner[i]);
        }
        for (int i = 0; i < gridManager.List_Decorations.Count; i++)
        {
            Destroy(gridManager.List_Decorations[i]);
        }
    }
}
