using System.Collections;
using UnityEngine;

public class MartilloGigante : MonoBehaviour
{
    private Animator animator;
    private float escalaOriginalX;
    private float timeBtwAttack;

    [Header("Velocidad de ataque")]
    private float attackCooldown = 2.5f;
    public Transform attackPos;
    public LayerMask whatIsEnemies;

    [Header("Rango de ataque")]
    public float attackRange;

    [Header("Daño")]
    public int damage;

    private Collider2D attackCollider; // Referencia al collider de ataque

    void Start()
    {
        // Guarda la escala original en el eje X
        escalaOriginalX = transform.localScale.x;
        animator = GetComponent<Animator>();
        // Obtiene el collider de ataque
        attackCollider = GetComponent<Collider2D>();
        // Desactiva el collider de ataque al inicio
        attackCollider.enabled = false;
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                timeBtwAttack = attackCooldown;
                animator.SetBool("MartilloAttackbool", true);
                StartCoroutine(ResetMartilloAfterDelay());
                Debug.Log("Atacando...");
                StartCoroutine(AttackWithDelay());
                
            }
        }
        else
        {
            // Disminuir el tiempo de espera
            timeBtwAttack -= Time.deltaTime;
        }

        // Verifica la rotación del objeto para invertir la escala en el eje X según sea necesario
        float angulo = transform.rotation.eulerAngles.z;
        if (angulo < 90f)
        {
            transform.localScale = new Vector3(escalaOriginalX * -1f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(escalaOriginalX, transform.localScale.y, transform.localScale.z);
        }
    }

    private IEnumerator AttackWithDelay()
    {
        // Activa el collider de ataque al iniciar el ataque
        attackCollider.enabled = true;

        yield return new WaitForSeconds(0.75f);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i] != null && enemiesToDamage[i].GetComponent<EnemyHealth>() != null)
            {
                enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
                Debug.Log("Enemigo Atacado");
            }
        }

        // Desactiva el collider de ataque una vez que el ataque ha terminado
        attackCollider.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator ResetMartilloAfterDelay()
    {
        // Espera 0.5 segundos
        yield return new WaitForSeconds(0.755f);

        // Establece el valor del parámetro "HachaAttack" en false
        if (animator != null)
        {
            animator.SetBool("MartilloAttackbool", false);
        }
    }
}
