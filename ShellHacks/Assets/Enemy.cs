using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int attackPower = 10;
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            TakeDamage(50);
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy takes " + damage + " damage. Health is now: " + currentHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void DealDamage(Player player)
    {
        player.TakeDamage(attackPower);
        Debug.Log("Enemy dealt " + attackPower + " damage.");
    }
}
