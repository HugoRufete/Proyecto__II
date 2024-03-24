using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 3f;
    private Animator animator

    private void Start()
    {
        health = maxHealth;
        animator = GetComponent
    }

    public void EnemyTakeDamage (int  damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            
            Destroy(gameObject);
        }
    }
}
