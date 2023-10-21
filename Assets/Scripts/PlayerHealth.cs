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

    void Start()
    {
        currentHealth = maxHealth;
    }

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            PlayerDead.Play();
            Die(); // You can implement a "Die" method to handle player death.
        } else {
            PlayerHurt.Play();
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

