using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Transform player;
    private int enemyDamage = 1;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assign the player's transform
    }

    void Update()
    {
        // Move towards the player
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Deal damage
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(enemyDamage);
                Destroy(collision.gameObject);
            }

               
        }
        
    }
}
