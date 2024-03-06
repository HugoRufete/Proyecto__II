using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timeBtwAttack;
    public float cooldownTime = 0.5f; // Tiempo de espera entre ataques
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Atacando...");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
                    Debug.Log("Enemigo Atacado");
                }

                // Establecer el tiempo de espera antes del próximo ataque
                timeBtwAttack = cooldownTime;
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
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
