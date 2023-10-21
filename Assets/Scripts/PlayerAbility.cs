using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public GameObject target_player;
    //Healling Variables
    public PlayerController playerMovement; // Reference to the PlayerMovement script
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public float healingRate = 1f; // Healing rate in HP per second
    private bool isHealing = false;
    private float timer = 0;

    //Teleport Variables
    public float cooldownTime = 5f; // Cooldown time in seconds
    private bool canTeleport = true;
    void Update()
    {
        if (isHealing)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isHealing)
            {
                // Stop healing and restore normal speed
                timer = 0;
                StopHealing();
                //Debug.Log("Heal Stop");
            }
            else
            {
                // Start healing and reduce speed
                StartHealing();
                //Debug.Log("Heal Start");
            }
        }

        // If in the healing state, heal the player
        if (isHealing && timer > 2.0f)
        {
            //Debug.Log("Heal Performed");
            playerHealth.Heal(1);
            timer = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && canTeleport)
        {
            // Teleport to the mouse position
            TeleportToMouse();
        }

        if (Input.GetKey(KeyCode.R))
        {
            Freeze();
        }
    }

    //code for Healing
    void StartHealing()
    {
        // Check if the player's health is not already at the maximum
        //Debug.Log("Speed Change!");
        isHealing = true;
        playerMovement.SetSpeed(1f); // Set player's speed to 1 (or any other reduced speed)
    }

    void StopHealing()
    {
        isHealing = false;
        playerMovement.RestoreSpeed(); // Restore the player's original speed
    }


    //code for Teleport
    void TeleportToMouse()
    {
        // Get the current mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        Debug.Log("Mouse Position: " + mousePosition);
        
        // Teleport the player to the mouse position
        transform.position = mousePosition;

        // Disable teleportation temporarily while on cooldown
        canTeleport = false;
        StartCoroutine(TeleportCooldown());
    }
    IEnumerator TeleportCooldown()
    {
        // Wait for the cooldown time
        yield return new WaitForSeconds(cooldownTime);

        // Re-enable teleportation
        canTeleport = true;
    }

    public void Freeze()
    {

    }
}