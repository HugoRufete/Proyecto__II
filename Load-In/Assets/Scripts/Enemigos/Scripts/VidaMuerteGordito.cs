using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaMuerteGordito : MonoBehaviour
{
    public float health;
    public float maxHealth = 3f;

    private void Start()
    {
        health = maxHealth;
    }

    public void EnemyTakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
