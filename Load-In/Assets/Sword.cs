using UnityEngine;

public class Sword : MonoBehaviour
{
    public int damageAmount = 1;  // Cantidad de daño infligido por el arma melee
    public float empujeForce = 5f;  // Fuerza de empuje aplicada a los enemigos al golpear
    public float rangoAtaque = 2f;  // Rango de ataque del arma melee

    void Update()
    {
        // Ejemplo: Presionar la tecla "Q" para activar el arma melee
        if (Input.GetButtonDown("Fire1"))
        {
            ActivarArmaMelee();
        }
    }

    void OnDrawGizmos()
    {
        // Visualiza el rango de ataque en el editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }

    void ActivarArmaMelee()
    {
        // Coloca aquí cualquier lógica adicional que desees realizar al activar el arma melee

        // Llama a la función MeleeAttack para realizar el ataque melee
        MeleeAttack();
    }

    void MeleeAttack()
    {
        // Realiza el ataque melee solo si el objetivo está dentro del rango
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, rangoAtaque);

        foreach (Collider2D collider in colliders)
        {
            // Verifica si la colisión es con un objeto que tiene el script EnemyHealth
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Si es un enemigo, inflige daño y aplica empuje
                enemyHealth.EnemyTakeDamage(damageAmount);

                // Calcula la dirección del empuje (en este ejemplo, siempre empuja en la dirección del arma)
                Vector2 direccionEmpuje = (collider.transform.position - transform.position).normalized;

                // Aplica el empuje al enemigo
                Rigidbody2D rbEnemigo = collider.GetComponent<Rigidbody2D>();
                if (rbEnemigo != null)
                {
                    rbEnemigo.AddForce(direccionEmpuje * empujeForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
