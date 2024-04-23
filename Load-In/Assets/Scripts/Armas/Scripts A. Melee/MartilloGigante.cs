using System.Collections;
using UnityEngine;

public class MartilloGigante : MonoBehaviour
{
    private Animator animator;
    private float escalaOriginalX;
    private float timeBtwAttack;

    [Header("Velocidad de ataque")]
    public float attackCooldown; // Tiempo de espera entre ataques
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    [Header("Rango de ataque")]
    public float attackRange;

    [Header("Daño")]
    public int damage;

    void Start()
    {
        // Guarda la escala original en el eje X
        escalaOriginalX = transform.localScale.x;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 updatedAttackPos = attackPos.position;

        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Atacando...");
                Attack();
            }
        }
        else
        {
            // Disminuir el tiempo de espera
            timeBtwAttack -= Time.deltaTime;
        }

        // Verifica si el objeto ha rotado menos de -90 grados
        float angulo = transform.rotation.eulerAngles.z;
        if (angulo < 90f)
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

    private void Attack()
    {
        animator.SetBool("MartilloAttackbool", true);
        StartCoroutine(ResetMartilloAttackAfterDelay());
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
            Debug.Log("Enemigo Atacado");
        }

        // Establecer el tiempo de espera antes del próximo ataque
        timeBtwAttack = attackCooldown;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator ResetMartilloAttackAfterDelay()
    {
        // Espera 0.5 segundos
        yield return new WaitForSeconds(0.5f);

        // Establece el valor del parámetro "HachaAttack" en false
        if (animator != null)
        {
            animator.SetBool("MartilloAttackbool", false);
        }
    }
}
