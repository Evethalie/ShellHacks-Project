using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    public int attackPower = 10;
    public HealthBar healthBar;
   public Animator animator;

    public List<MainAbility> abilities;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        
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
