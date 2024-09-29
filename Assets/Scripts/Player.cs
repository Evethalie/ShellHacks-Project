using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int attackPower = 10;
    public HealthBar healthBar;

    public ScriptableObject[] abilities = new ScriptableObject[5];

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(50);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player takes " + damage + " damage. Health is now: " + currentHealth);
        healthBar.SetHealth(currentHealth);
    }

    public void DealDamage(Enemy enemy)
    {
        enemy.TakeDamage(attackPower);
        Debug.Log("Player dealt " + attackPower + " damage.");
    }
}
