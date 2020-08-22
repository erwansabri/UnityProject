using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    GameObject player;

    public float speed;
    Rigidbody2D rb;
    public Vector2 directionPlayer;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        directionPlayer = (player.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector2(directionPlayer.x, directionPlayer.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
