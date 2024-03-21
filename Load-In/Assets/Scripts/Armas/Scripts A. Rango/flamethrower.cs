using UnityEngine;

public class flamethrower : MonoBehaviour
{
    private float anguloMinimo = 90f;
    private float escalaOriginalX;
    private bool isAttacking = false;

    [Header("Munici�n")]
    public int ammoCount = 10; 

    

    [Header("Velocidad de ataque")]
    public float attackCooldown; // Tiempo de espera entre ataques
    private float lastAttackTime;
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    [Header("Rango de ataque")]
    public float attackRange;

    [Header("Da�o")]
    public int damage;
    public int damagePerSecond; 

    void Start()
    {
        // Guarda la escala original en el eje X
        escalaOriginalX = transform.localScale.x;
    }

    private void Update()
    {
        Vector3 updatedAttackPos = attackPos.position;

        if (Input.GetKeyDown(KeyCode.Space) && ammoCount > 0)
        {
            // Si el jugador pulsa el bot�n y a�n tiene munici�n, realiza un solo ataque
            SingleAttack();
        }

        if (Input.GetKey(KeyCode.Space) && ammoCount > 0)
        {
            // Si el jugador mantiene pulsado el bot�n y a�n tiene munici�n, realiza ataques constantes por segundo
            ContinuousAttack();
        }
        else
        {
            isAttacking = false;
            CancelInvoke(); // Cancela el ataque constante si se deja de mantener pulsado el bot�n
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            RecargarLanzallamas();
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
            ammoCount--; // Se gasta una unidad de munici�n
        }
    }

    void ContinuousAttack()
    {
        if (!isAttacking)
        {
            // Comienza el ataque constante
            isAttacking = true;
            InvokeRepeating("DealDamageOverTime", 0f, 1f); // Invoca el m�todo cada segundo
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

        ammoCount--; // Se gasta una unidad de munici�n cada segundo
        if (ammoCount <= 0)
        {
            // Si se queda sin munici�n, cancela el ataque constante
            CancelInvoke("DealDamageOverTime");
        }
    }

    public void RecargarLanzallamas()
    {
        ammoCount = ammoCount + 10;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
