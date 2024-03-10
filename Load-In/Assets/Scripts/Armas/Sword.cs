using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timeBtwAttack;

    [Header("Velodidad de ataque")]
    public float attackCooldown; // Tiempo de espera entre ataques
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    [Header("Rango de ataque")]
    public float attackRange;

    [Header("Daño")]
    public int damage;

    private void Update()
    {
        
        Vector3 updatedAttackPos = attackPos.position;

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Atacando...");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(updatedAttackPos, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
                    Debug.Log("Enemigo Atacado");
                }

                // Establecer el tiempo de espera antes del próximo ataque
                timeBtwAttack = attackCooldown;
            }
        }
        else
        {
            // Disminuir el tiempo de espera
            timeBtwAttack -= Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 updatedAttackPos = attackPos.position;

        Gizmos.DrawWireSphere(updatedAttackPos, attackRange);
    }
}
