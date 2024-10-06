using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class InkController : MonoBehaviour
{
    private Transform TRA_Target;
    [NonSerialized] public Transform TRA_Space;
    private float FLO_CurrentAlpha;
    private float FLO_SecondAlpha;

    private BoxCollider2D BoxCollider2D;
    
    
    
    private void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        TRA_Target = BallManager.Instance.LIST_Ball[0].transform;
        StartCoroutine(SpaceU());
    }

    private IEnumerator GoToPlayer()
    {
        yield return new WaitForSeconds(Random.Range(0.3f, 1f));
        BoxCollider2D.enabled = true;
        while (FLO_CurrentAlpha < 1)
        {
            transform.position = Vector2.Lerp(transform.position, TRA_Target.position, FLO_CurrentAlpha);
            FLO_CurrentAlpha += Time.deltaTime;
            yield return null;
        }
        transform.position = TRA_Target.position;
    }

    private IEnumerator SpaceU()
    {
        while (FLO_SecondAlpha < 1)
        {
            transform.position = Vector2.Lerp(transform.position, TRA_Space.position, FLO_SecondAlpha);
            FLO_SecondAlpha += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(GoToPlayer());

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().GetInk();
            Destroy(gameObject);
        }
    }
}
