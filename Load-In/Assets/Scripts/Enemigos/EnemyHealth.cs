using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 20;
    private bool additionalDamageActivated = false; // Variable para indicar si se ha activado el da�o adicional
    private float additionalDamageMultiplier = 5.0f; // Multiplicador de da�o adicional


    private Vector2 initialPosition; // Posici�n inicial del enemigo
    private bool isPushed = false; // Bandera para indicar si el enemigo est� siendo empujado
    private bool hasBeenPushed = false; // Bandera para indicar si el enemigo ha sido empujado al menos una vez

    private Rigidbody2D rb; // Componente Rigidbody2D del enemigo

    private void Start()
    {
        health = maxHealth;
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>(); // Obtener el componente Rigidbody2D del enemigo
    }

    public void EnemyTakeDamage(int damageAmount)
    {
        if (additionalDamageActivated) // Verifica si el da�o adicional est� activado
        {
            damageAmount = Mathf.RoundToInt(damageAmount * additionalDamageMultiplier); // Aplica el da�o adicional
        }

        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void PushBack(Vector2 direction, float force, float maxDistance)
    {
        if (!hasBeenPushed) // Verifica si el enemigo ha sido empujado al menos una vez
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Reinicia la velocidad para evitar acumulaci�n
                rb.AddForce(direction * force, ForceMode2D.Impulse);

                hasBeenPushed = true; // Marca el enemigo como empujado al menos una vez
                isPushed = true; // Marca el enemigo como empujado
            }
        }
        else // Si ya ha sido empujado
        {
            // Calcula la distancia entre la posici�n inicial y la posici�n actual
            float distance = Vector2.Distance(initialPosition, transform.position);

            // Detiene el empuje si la distancia alcanza el l�mite m�ximo
            if (distance >= maxDistance)
            {
                rb.velocity = Vector2.zero; // Detener el movimiento
                isPushed = false; // Reinicia la bandera para permitir futuros empujes
            }
        }
    }

    public void ActivateAdditionalDamage()
    {
        additionalDamageActivated = true;
        additionalDamageMultiplier = 1.1f; // Aumenta el da�o recibido en un 10%
    }

    

}
