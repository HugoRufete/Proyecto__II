using UnityEngine;

public class Bala : MonoBehaviour
{
    public int damageAmount = 1;  // Cantidad de da�o infligido por la bala

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica si la colisi�n es con un objeto que tiene el script EnemyHealth
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            // Si es un enemigo, inflige da�o y destruye la bala
            enemyHealth.EnemyTakeDamage(damageAmount);
            Destroy(gameObject);
        }
        else
        {
            // Si la colisi�n no es con un enemigo, solo destruye la bala
            Destroy(gameObject);
        }
    }
}
