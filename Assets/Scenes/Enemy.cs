using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float speed = 10f;

    protected Rigidbody2D enemyRb;
    protected GameObject player;

    
    // Start is called before the first frame update
    protected virtual void Start()
    {

        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        if(player == null)
        {
            Debug.LogError("Player GameObject not found");
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (player != null)
        {
            FollowPlayer();
        }
    }

    protected virtual void FollowPlayer()
    {
        if (!MainManager.Instance.GameOver())
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Vector2 movement = direction * speed * Time.deltaTime;
            enemyRb.MovePosition(enemyRb.position + movement);
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
        }
    }
}
