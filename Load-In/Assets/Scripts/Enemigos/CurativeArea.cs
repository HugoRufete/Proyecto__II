using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurativeArea : MonoBehaviour
{
    public float healingAmount = 1.5f;
    public float healingInterval = 1f;

    // Almacena referencias a los coroutines activos
    private Dictionary<GameObject, Coroutine> activeCoroutines = new Dictionary<GameObject, Coroutine>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Comienza la curación para el enemigo
            Coroutine coroutine = StartCoroutine(HealEnemyOverTime(collision.gameObject));
            activeCoroutines.Add(collision.gameObject, coroutine);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Detiene la curación para el enemigo que sale del área
            if (activeCoroutines.ContainsKey(collision.gameObject))
            {
                StopCoroutine(activeCoroutines[collision.gameObject]);
                activeCoroutines.Remove(collision.gameObject);
            }
        }
    }

    private IEnumerator HealEnemyOverTime(GameObject enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            while (true)
            {
                enemyHealth.HealEnemy(healingAmount);
                yield return new WaitForSeconds(healingInterval);
            }
        }
    }
}