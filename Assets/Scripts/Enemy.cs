using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 500;
    public int currentHealth;
    public int attackPower;
    public int attackMax = 50;
    public HealthBar healthBar;
    public bool dealingDamage = false;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    private void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy takes " + damage + " damage. Health is now: " + currentHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void DealDamage(Player player)
    {
        attackPower = Random.Range(10, attackMax);
        player.TakeDamage(attackPower);
        Debug.Log("Enemy dealt " + attackPower + " damage.");
        dealingDamage = true;
    }
}
