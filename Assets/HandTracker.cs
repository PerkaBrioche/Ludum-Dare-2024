using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandTracker : MonoBehaviour
{
    public static HandTracker Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public Transform Player;
    public bool IsFollowing;

    public GameObject OBJ_Hand;
    public bool TryingToSmash;


    public void InitliazeHand()
    {
        TryingToSmash = true;
    }
    public void StopHand()
    {
        StopAllCoroutines();
        TryingToSmash = false;
    }

    private void Start()
    {
        TryingToSmash = true;
    }

    private void Update()
    {
        if (IsFollowing)
        {
            FollowPlayer();
        }

        if (TryingToSmash)
        {
            TryingToSmash = false;
            StartCoroutine(RandomSeconds());
        }
    }

    public void FollowPlayer()
    {
        transform.position = Player.position;
    }

    public void SmashHand()
    {
        Instantiate(OBJ_Hand, transform.position, OBJ_Hand.transform.rotation);
    }

    public IEnumerator RandomSeconds()
    {
        yield return new WaitForSeconds(Random.Range(5,17));
        SmashHand();
        TryingToSmash = true;
    }
}
