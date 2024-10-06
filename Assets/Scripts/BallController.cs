using UnityEngine;

public class BallController : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3F;
    public float SpaceFloat = 0.3F; 
    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        Vector2 playerDirection = GetPlayerFacingDirection();
        if (PlayerController.Instance.IsMoving())
        {
            transform.position = Vector2.SmoothDamp(transform.position, GetSpace(0), ref velocity, smoothTime);
        }
        else
        {
            Vector2 behindPlayerPosition = (Vector2)target.position - playerDirection * SpaceFloat;
            transform.position = Vector2.SmoothDamp(transform.position, behindPlayerPosition, ref velocity, smoothTime);
        }
    }

    private Vector2 GetPlayerFacingDirection()
    {
        return PlayerController.Instance.GetFacingDirection().normalized;
    }

    private Vector2 GetSpace(float space)
    {
        return target.TransformPoint(new Vector2(space, space));
    }
}