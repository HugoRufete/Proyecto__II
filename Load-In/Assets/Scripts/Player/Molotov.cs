using UnityEngine;

public class Molotov : MonoBehaviour
{
    public float explosionRadius = 2f; // Radio de la explosi�n
    public int damageAmount = 20; // Cantidad de da�o infligido

    void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    public void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                collider.GetComponent<EnemyHealth>().EnemyTakeDamage(damageAmount);
            }
        }

        // Destruir el objeto arrojadizo (Molotov) despu�s de la explosi�n
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
