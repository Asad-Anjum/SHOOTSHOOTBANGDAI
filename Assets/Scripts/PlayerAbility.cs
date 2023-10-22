using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public GameObject target_player;
    //Healling Variables
    [Header("Healing Variables")]
    public PlayerController playerMovement; // Reference to the PlayerMovement script
    public PlayerHealth playerHealth; // Reference to the PlayerHealth script
    public float healingRate = 1f; // Healing rate in HP per second
    private bool isHealing = false;
    private float timer = 0;
    public AudioSource HealStart;
    public AudioSource HealEnd;

    

    //Teleport Variables
    [Header("Teleport Variables")]
    public float cooldownTime = 5f; // Cooldown time in seconds
    private bool canTeleport = true;
    public AudioSource teleportSound;
    public GameObject teleportParticlesObject;
    private ParticleSystem teleportParticles;

    //Freeze Variables
    [Header("Freeze Variables")]
    public float FreezecooldownTime = 10f; // Cooldown time in seconds
    public float freezeDuration = 3f; // Duration of the freeze effect
    public AudioSource FreezeSound;


    private bool canUseFreeze = true;

    void Start()
    {
        teleportParticles = teleportParticlesObject.GetComponent<ParticleSystem>();
    }
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
            teleportParticles.Play();
            TeleportToMouse();
        }

         if (Input.GetKeyDown(KeyCode.R) && canUseFreeze)
        {
            // Activate the freeze ability
            ActivateFreeze();
        }
    }

    //code for Healing
    void StartHealing()
    {
        // Check if the player's health is not already at the maximum
        //Debug.Log("Speed Change!");
        HealStart.Play();
        isHealing = true;
        playerMovement.SetSpeed(1f); // Set player's speed to 1 (or any other reduced speed)
    }

    void StopHealing()
    {
        HealEnd.Play();
        isHealing = false;
        playerMovement.RestoreSpeed(); // Restore the player's original speed
    }


    //code for Teleport
    void TeleportToMouse()
    {
        // Get the current mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        //Debug.Log("Mouse Position: " + mousePosition);
        
        // Teleport the player to the mouse position
        transform.position = mousePosition;
        teleportSound.Play();

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

    //code for freeze
    void ActivateFreeze()
    {
        // Find all enemy objects in the scene
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();

        // Freeze the movement of each enemy
        foreach (EnemyAI enemy in enemies)
        {
            enemy.FreezeMovement(freezeDuration);
        }
        FreezeSound.Play();
        // Disable the freeze ability temporarily while on cooldown
        canUseFreeze = false;
        StartCoroutine(FreezeCooldown());
    }

    IEnumerator FreezeCooldown()
    {
        // Wait for the cooldown time
        yield return new WaitForSeconds(FreezecooldownTime);

        // Re-enable the freeze ability
        canUseFreeze = true;
    }
}
