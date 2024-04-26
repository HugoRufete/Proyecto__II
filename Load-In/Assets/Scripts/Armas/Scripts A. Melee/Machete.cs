using UnityEngine;

public class Machete : MonoBehaviour
{

    private float anguloMinimo = 90f;
    private float escalaOriginalX;

    private float timeBtwAttack;

    [Header("Velodidad de ataque")]
    public float attackCooldown; 
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    [Header("Rango de ataque")]
    public float attackRange;

    [Header("Daño")]
    public float damage;
    private float damageBuffed = 1;

    void Start()
    {
        // Guarda la escala original en el eje X
        escalaOriginalX = transform.localScale.x;
    }

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
                    enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage * damageBuffed);
                    Debug.Log("Enemigo Atacado");
                }

                // Establecer el tiempo de espera antes del próximo ataque
                timeBtwAttack = attackCooldown;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        float angulo = transform.rotation.eulerAngles.z;

        if (angulo < anguloMinimo)
        {
            transform.localScale = new Vector3(escalaOriginalX * -1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(escalaOriginalX, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 updatedAttackPos = attackPos.position;

        Gizmos.DrawWireSphere(updatedAttackPos, attackRange);
    }

    public void AumentarDañoMachete()
    {
        damageBuffed = damageBuffed * 2f;
    }
}
