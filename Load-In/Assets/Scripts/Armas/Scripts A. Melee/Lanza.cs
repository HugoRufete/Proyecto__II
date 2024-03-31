using UnityEngine;

public class Lanza : MonoBehaviour
{
    private float escalaOriginalX;
    private float timeBtwAttack;
    private Animator animator;

    [Header("Velocidad de ataque")]
    public float attackCooldown; // Tiempo de espera entre ataques
    public Collider2D attackCollider; // Collider que determina la zona de ataque
    public LayerMask whatIsEnemies;

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
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Atacando...");
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Ataque_Alabarda")) // Comprobar si la animación no está en reproducción
                {
                    Attack();
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

    private void Attack()
    {
        animator.Play("Ataque_Alabarda");
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCollider.transform.position, attackCollider.bounds.extents.x, whatIsEnemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyHealth>().EnemyTakeDamage(damage);
            Debug.Log("Enemigo Atacado");
        }

        // Establecer el tiempo de espera antes del próximo ataque
        timeBtwAttack = attackCooldown;
    }
}
