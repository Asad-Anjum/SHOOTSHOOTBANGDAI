using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public TMP_Text healthText;
    public AudioSource PlayerHurt;
    public AudioSource PlayerDead;
    
    //screenEffect reference
    private CameraEffect screen;

    void Start()
    {
        currentHealth = maxHealth;
        screen = Camera.main.GetComponent<CameraEffect>();
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        screen.Shake();
        if (currentHealth <= 0)
        {
            PlayerDead.Play();
            Die(); // You can implement a "Die" method to handle player death.
        } else {
            PlayerHurt.Play();
            if (currentHealth < 3)
            {
                screen.Flash();
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Heal(int amount)
    {
        // Ensure the player's health doesn't exceed the maximum
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
        //Debug.Log("Heal Performed");
    }
}

