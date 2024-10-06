using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // La cible que l'ennemi suit (par ex. le joueur)
    public float speed = 200f; // Vitesse de l'ennemi
    public float updatePathRate = 0.2f; // Fréquence de recalcul du chemin
    public float repathDistance = 0.5f; // Recalcule le chemin si l'écart par rapport à la trajectoire est trop grand

    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerManager>().transform;

        // Commence le recalcul du chemin
        InvokeRepeating("UpdatePath", 0f, updatePathRate);
    }

    void UpdatePath()
    {
        // Recalcule toujours le chemin vers la position du joueur
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        // Direction vers le prochain waypoint (ou ajuster avec une interpolation vers la destination finale)
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        // Si l'ennemi se trouve trop loin de la trajectoire, recalculer un nouveau chemin
        if (Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]) > repathDistance)
        {
            UpdatePath();
        }

        // Vérifie s'il a atteint le waypoint actuel
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < 0.3f)
        {
            currentWaypoint++;
        }
    }
}
