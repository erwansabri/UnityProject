using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Animator anim;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    GameObject player;
    Vector2 position;
    PlayerMovement playerMovement;

    Seeker seeker;
    Rigidbody2D rb;
    private CapsuleCollider2D collider;

    public GameObject bullet;
    public GameObject firePoint;
    float fireRate;
    float nextFire;

    void PlayAnimation(float horizontal, float vertical, int idle, float speed)
    {
        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);
        anim.SetInteger("Idle", idle);
        anim.SetFloat("Speed", speed);
    }


    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, player.transform.position, OnPathComplete);

    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void ShootingToPlayer()
    {
        bool inSight = false;
        RaycastHit2D hitInfo;
        hitInfo = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized);
        if (hitInfo && hitInfo.transform.tag == "Player")
            inSight = true;

        if (Time.time > nextFire && inSight && !playerMovement.isDiscret)
        {
            collider.enabled = false;
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
            collider.enabled = true;
        }
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        position = (Vector2)player.transform.position;
        playerMovement = player.GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        fireRate = 1f;
        nextFire = Time.time;
    }


    void FixedUpdate()
    {

        //Movement 
        if (path == null)
            return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < 3 && position != (Vector2)player.transform.position && playerMovement.isDiscret == false)
            rb.AddForce(force);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        PlayAnimation(rb.velocity.x, rb.velocity.y, (direction.x < 0 ? 3 : 4), rb.velocity.SqrMagnitude());
        position = player.transform.position;

        ShootingToPlayer();

    }

}
