using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    private Transform player;
    private int enemyDamage = 1;

    //Freeze ability
    private bool isFrozen = false;
    private float originalSpeed;
    public AudioSource hurtSound;
    public GameObject freezeParticlesObject;
    private ParticleSystem freezeParticles;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Assign the player's transform
        originalSpeed = moveSpeed;
        freezeParticles = freezeParticlesObject.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Move towards the player
        if (!isFrozen)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Bullet")
        {
            // Deal damage

            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(enemyDamage);
                Destroy(collision.gameObject);
                hurtSound.Play();
            }

               
        }
        
    }

    public void FreezeMovement(float duration)
    {
        // Freeze the enemy's movement for the specified duration.
        isFrozen = true;
        moveSpeed = 0; // Set speed to 0 to freeze movement temporarily.

        freezeParticles.Play();
        StartCoroutine(UnfreezeAfterDuration(duration));
    }

    IEnumerator UnfreezeAfterDuration(float duration)
    {
        // Wait for the specified duration and then unfreeze the enemy.
        yield return new WaitForSeconds(duration);

        isFrozen = false;
        moveSpeed = originalSpeed; // Restore the original speed.
    }
}
