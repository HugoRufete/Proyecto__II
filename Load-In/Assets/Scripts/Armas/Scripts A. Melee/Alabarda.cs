using System.Collections;
using UnityEngine;

public class Alabarda : MonoBehaviour
{
    private float escalaOriginalX;
    private float timeBtwAttack;
    private Animator animator;

    [Header("Velocidad de ataque")]
    public float attackCooldown; 
    public GameObject attackColliderObject; 
    public LayerMask whatIsEnemies;

    [Header("Daño")]
    public int damage;

    private Collider2D attackCollider;

    void Start()
    {
        escalaOriginalX = transform.localScale.x;
        animator = GetComponent<Animator>();

        attackCollider = attackColliderObject.GetComponent<Collider2D>();

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
            if (Input.GetButtonDown("Fire1"))
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
            timeBtwAttack -= Time.deltaTime;
        }

    }

    private IEnumerator AttackWithDelay()
    {
        yield return new WaitForSeconds(0.3f);


        if (attackCollider != null)
        {
            attackCollider.enabled = true;

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

            attackCollider.enabled = false;
        }
        else
        {
            Debug.LogWarning("El collider de ataque no está asignado correctamente.");
        }

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

        if (animator != null)
        {
            animator.SetBool("AlabardaAttackbool", false);
        }
    }
}
