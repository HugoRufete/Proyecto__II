using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 20;
    private bool additionalDamageActivated = false;
    private float additionalDamageMultiplier = 5.0f; // Multiplicador de daño adicional
    public GameObject experiencePrefab; // Prefab del item de experiencia
    public int experienceItemsCount = 5; // Cantidad de items de experiencia a soltar
    public float maxDistanceFromEnemy = 0.5f; // Máxima distancia del enemigo para soltar el item de experiencia

    private Vector2 initialPosition;
    private bool isPushed = false;
    private bool hasBeenPushed = false;

    private Rigidbody2D rb;

    private void Start()
    {
        health = maxHealth;
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    public void EnemyTakeDamage(int damageAmount)
    {
        if (additionalDamageActivated)
        {
            damageAmount = Mathf.RoundToInt(damageAmount * additionalDamageMultiplier);
        }

        health -= damageAmount;

        if (health <= 0)
        {
            DropExperienceItems(transform.position); // Llamamos a la función y pasamos la posición del enemigo
            Destroy(gameObject);
        }
    }

    private void DropExperienceItems(Vector2 enemyPosition)
    {
        for (int i = 0; i < experienceItemsCount; i++)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector2 spawnPosition = enemyPosition + randomDirection * Random.Range(0f, maxDistanceFromEnemy);
            Instantiate(experiencePrefab, spawnPosition, Quaternion.identity);
        }
    }

    public void PushBack(Vector2 direction, float force, float maxDistance)
    {
        if (!hasBeenPushed)
        {
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(direction * force, ForceMode2D.Impulse);

                hasBeenPushed = true;
                isPushed = true;
            }
        }
        else
        {

            float distance = Vector2.Distance(initialPosition, transform.position);

            if (distance >= maxDistance)
            {
                rb.velocity = Vector2.zero;
                isPushed = false;
            }
        }
    }

    public void ActivateAdditionalDamage()
    {
        additionalDamageActivated = true;
        additionalDamageMultiplier = 1.1f; // Aumenta el daño recibido en un 10%
    }
}
