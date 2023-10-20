using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerDamage = 1; // Player's damage value
    public float invincibilityTime = 1.0f; // Time of invincibility after taking damage
    private bool isInvincible = false;
    private float invincibilityTimer = 0.0f;

    public float moveSpeed = 5.0f;
    public float dashSpeed = 10.0f;
    public TrailRenderer trailRenderer;

    private bool isDashing = false;

    void Update()
    {
        // Player movement
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1.0f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1.0f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1.0f;
        }

        // Normalize the movement vector to ensure consistent speed in all directions
        movement.Normalize();

        
        Vector3 newPosition = transform.position + new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.deltaTime;
        transform.position = newPosition;

        // Player rotation towards the mouse cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Ensure the object is facing the correct direction **ADDED AFTER DEMO 1
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (direction.x > 0 ? 1 : -1);
        transform.localScale = scale;

        // Dash action with "Shift" key
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!isDashing)
            {
                isDashing = true;
                StartCoroutine(Dash(movement));
            }
        }

        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }

    IEnumerator Dash(Vector2 movement)
    {
        trailRenderer.enabled = true; // Enable TrailRenderer while dashing
        Vector3 originalPosition = transform.position;
        Vector3 dashDirection = new Vector3(movement.x, movement.y, 0).normalized;
        Vector3 dashEndPosition = originalPosition + dashDirection * dashSpeed;

        float dashDuration = 0.2f; // Adjust this value to control dash duration
        float elapsedTime = 0;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, dashEndPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
        trailRenderer.enabled = false; // Disable TrailRenderer after dashing
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isInvincible)
        {
            if (collision.tag == "Enemy")
            {
                // Deal damage to the player
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(playerDamage);
                    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(playerDamage); //ADDED 1 LINE HERE
                }

                // Make the player invincible for a short duration
                isInvincible = true;
                invincibilityTimer = invincibilityTime;

                // Handle other effects for the player, such as knockback or temporary invincibility frames.
            }
        }
    }

}
