using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemies : MonoBehaviour
{
    public int damageAmount;
    private void OnTriggerEnter2D (Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.EnemyTakeDamage(damageAmount);
        }
    }   
    
}
    

