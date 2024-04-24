using System.Collections;
using UnityEngine;

public class Lanza : MonoBehaviour
{
    private float escalaOriginalX;
    private float timeBtwAttack;
    private Animator animator;

    [Header("Velocidad de ataque")]
    public float attackCooldown; // Tiempo de espera entre ataques
    public GameObject attackColliderObject; // Referencia al GameObject que contiene el collider de ataque
    public LayerMask whatIsEnemies;

    [Header("Daño")]
    public int damage;

    private Collider2D attackCollider; // Collider que determina la zona de ataque

    void Start()
    {
        // Guarda la escala original en el eje X
        escalaOriginalX = transform.localScale.x;
        animator = GetComponent<Animator>();

        // Obtener el collider de ataque del GameObject de ataque
        attackCollider = attackColliderObject.GetComponent<Collider2D>();

        // Desactivar el collider de ataque al inicio si está asignado correctamente
        if (attackCollider != null)
        {
            attackCollider.enabled = false;
        }
        else
        {
            Debug.LogError("Collider de ataque no encontrado en el GameObject de ataque.");
        }
    }

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("AlabardaAttackbool", true);
                StartCoroutine(ResetAlabardaAfterDelay());
                Debug.Log("Atacando...");
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("AlabardaAttack")) 
                {
                    StartCoroutine(AttackWithDelay());
                    timeBtwAttack = attackCooldown;
                }
            }
        }
        else
        {
            // Disminuir el tiempo de espera
            timeBtwAttack -= Time.deltaTime;
        }

        // Aquí puedes agregar la lógica para invertir la escala si es necesario
    }

    private IEnumerator AttackWithDelay()
    {
        yield return new WaitForSeconds(0.3f);


        if (attackCollider != null)
        {
            attackCollider.enabled = true;

            // Espera un tiempo para permitir que el collider detecte los enemigos
            yield return new WaitForSeconds(0.1f);

            
            Collider2D[] results = new Collider2D[7]; 
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = whatIsEnemies; 
            filter.useLayerMask = true;

            int count = attackCollider.OverlapCollider(filter, results); 

            for (int i = 0; i < count; i++)
            {
                Collider2D enemyCollider = results[i];
                if (enemyCollider != null && enemyCollider.CompareTag("Enemy"))
                {
                    enemyCollider.GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
                    Debug.Log("Enemigo Atacado");
                }
            }

            // Desactiva el collider de ataque después de un breve tiempo
            attackCollider.enabled = false;
        }
        else
        {
            Debug.LogWarning("El collider de ataque no está asignado correctamente.");
        }

        // Espera un momento antes de restablecer la animación de ataque
        yield return new WaitForSeconds(0.1f);
       
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (attackCollider != null)
        {
            Gizmos.DrawWireCube(attackCollider.bounds.center, attackCollider.bounds.size);
        }
    }

    private IEnumerator ResetAlabardaAfterDelay()
    {
        // Espera 0.5 segundos
        yield return new WaitForSeconds(0.755f);

        // Establece el valor del parámetro "HachaAttack" en false
        if (animator != null)
        {
            animator.SetBool("AlabardaAttackbool", false);
        }
    }
}
