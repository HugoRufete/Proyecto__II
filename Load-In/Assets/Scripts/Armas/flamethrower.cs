using UnityEngine;

public class flamethrower : MonoBehaviour
{
    private float anguloMinimo = 90f;
    private float escalaOriginalX;
    private bool isAttacking = false;

    private float timeBtwAttack;
    private float lastAttackTime;

    [Header("Velocidad de ataque")]
    public float attackCooldown; // Tiempo de espera entre ataques
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    [Header("Rango de ataque")]
    public float attackRange;

    [Header("Daño")]
    public int damage;
    public int damagePerSecond; // Daño por segundo

    void Start()
    {
        // Guarda la escala original en el eje X
        escalaOriginalX = transform.localScale.x;
    }

    private void Update()
    {
        Vector3 updatedAttackPos = attackPos.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Si el jugador pulsa el botón, realiza un solo ataque
            SingleAttack();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // Si el jugador mantiene pulsado el botón, realiza ataques constantes por segundo
            ContinuousAttack();
        }
        else
        {
            isAttacking = false;
            CancelInvoke(); // Cancela el ataque constante si se deja de mantener pulsado el botón
        }

        float angulo = transform.rotation.eulerAngles.z;

        // Verifica si el objeto ha rotado menos de -90 grados
        if (angulo < anguloMinimo)
        {
            // Invierte la escala en el eje X
            transform.localScale = new Vector3(escalaOriginalX * -1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            // Restaura la escala original
            transform.localScale = new Vector3(escalaOriginalX, transform.localScale.y, transform.localScale.z);
        }
    }

    void SingleAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            Debug.Log("Atacando...");

            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
                Debug.Log("Enemigo Atacado");
            }

            lastAttackTime = Time.time;
        }
    }

    void ContinuousAttack()
    {
        if (!isAttacking)
        {
            // Comienza el ataque constante
            isAttacking = true;
            InvokeRepeating("DealDamageOverTime", 0f, 1f); // Invoca el método cada segundo
        }
    }

    void DealDamageOverTime()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damagePerSecond);
            Debug.Log("Enemigo Atacado por Segundo");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
