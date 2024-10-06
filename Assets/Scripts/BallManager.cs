using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager Instance;

    public GameObject OBJ_Ball;
    public Transform TRA_Player;

    public List<BallController> LIST_Ball;
    public List<float> LIST_SpaceFloat;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InkManager.Instance.NewPAlier();
        }
        if (Input.GetKeyDown("e"))
        {
            DestroyBall();
        }
    }

    public void SpawnBall()
    {
        print("SPAWWNNNNN");
        var BallInstance = Instantiate(OBJ_Ball, TRA_Player.position, OBJ_Ball.transform.rotation, transform);
        BallInstance.GetComponent<BallController>().SpaceFloat = LIST_SpaceFloat[LIST_Ball.Count];
        LIST_Ball.Add( BallInstance.GetComponent<BallController>());
        BallInstance.GetComponent<BallController>().target = TRA_Player;
        BallInstance.GetComponent<BallController>().smoothTime = 0.2f * LIST_Ball.Count;
    }

    public void DestroyBall()
    {
        if (LIST_Ball.Count > 1)
        {
            if (LIST_Ball[LIST_Ball.Count-1] != null)
            {
                Destroy(LIST_Ball[LIST_Ball.Count -1].gameObject);
                LIST_Ball.RemoveAt(LIST_Ball.Count -1);
            }
        }
    }

    public void BallFall()
    {
        if (LIST_Ball.Count > 1)
        {
            LIST_Ball[LIST_Ball.Count-1].Fall();
            LIST_Ball.RemoveAt(LIST_Ball.Count -1);
        }
        else
        {
            LIST_Ball[0].Fall();
            PlayerController.Instance.Dead();
            LIST_Ball.RemoveAt(LIST_Ball.Count);
        }

    }
}
