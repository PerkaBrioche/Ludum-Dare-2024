using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target; // La cible que l'ennemi suit (par ex. le joueur)
    public float speed = 200f; // Vitesse de l'ennemi
    public float updatePathRate = 0.2f; // Fréquence de recalcul du chemin
    public float repathDistance = 0.5f; // Recalcule le chemin si l'écart par rapport à la trajectoire est trop grand
    public float distancetest = 0.3f; // Recalcule le chemin si l'écart par rapport à la trajectoire est trop grand

    private Seeker seeker;
    private Rigidbody2D rb;
    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private bool facingRight = true; // Indique si l'ennemi fait face à droite
    
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerManager>().transform;

        InvokeRepeating("UpdatePath", 0f, updatePathRate);
    }

    void UpdatePath()
    {
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        // Flip the enemy to face the player
        Flip();

        if (Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]) > repathDistance)
        {
            UpdatePath();
        }
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < 0.3f)
        {
            currentWaypoint++;
        }
    }

    // Fonction Flip pour orienter l'ennemi vers le joueur
    private void Flip()
    {
        // Vérifie si l'ennemi est à droite ou à gauche du joueur
        if (target != null)
        {
            if ((rb.position.x < target.position.x && !facingRight) || (rb.position.x > target.position.x && facingRight))
            {
                // Si l'ennemi est à gauche du joueur, il doit regarder à droite, sinon à gauche
                facingRight = !facingRight; // On inverse la direction actuelle

                // Flipper visuellement l'ennemi en modifiant la rotation sur l'axe Y
                if (facingRight)
                {
                    FlipLeft();
                }
                else
                {
                    FlipRight();
                }
            }
        }
    }

    // Méthode pour flipper l'ennemi vers la droite
    private void FlipRight()
    {
        var ChildTransform = transform.GetChild(0).transform;
        ChildTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Méthode pour flipper l'ennemi vers la gauche
    private void FlipLeft()
    {
        var ChildTransform = transform.GetChild(0).transform;
        ChildTransform.rotation = Quaternion.Euler(0, 180, 0); // On inverse la rotation en Y
    }
}
