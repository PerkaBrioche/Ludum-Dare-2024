using UnityEngine;

public class ArmRotation : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = FindObjectOfType<PlayerManager>().transform;
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}