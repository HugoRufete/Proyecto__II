using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 20;
    private bool additionalDamageActivated = false;
    private float additionalDamageMultiplier = 5.0f;
    public GameObject experiencePrefab;

    [Header("Esporas")]
    public int cantidadEsporasDropeadas = 5;
    public float maxDistanceFromEnemy = 0.5f;

    private Vector2 initialPosition;
    private bool isPushed = false;
    private bool hasBeenPushed = false;

    public string animaciónCuración;

    private Rigidbody2D rb;
    private Animator animator;
    public string enemyDeadAnimationName = "NombrePorDefecto";

    bool experienciaSoltada;

    // Coroutine handle
    private Coroutine invulnerabilityCoroutine;

    // Declaración del evento estático
    public static event System.Action<int> enemyDeadEvent;

    private void Start()
    {
        health = maxHealth;
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void EnemyTakeDamage(float damageAmount)
    {
        if (!additionalDamageActivated)
        {
            health -= damageAmount;
        }

        if (health <= 0)
        {
            experienciaSoltada = true;
            if (enemyDeadEvent != null)
                enemyDeadEvent.Invoke(5);
            DropExperienceItems(transform.position);
            PlayAnimationIfHealthBelowZero();
            Destroy(gameObject);
        }
    }

    private void DropExperienceItems(Vector2 enemyPosition)
    {
        for (int i = 0; i < cantidadEsporasDropeadas; i++)
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
        Debug.Log("Daño Aumentado");
    }

    public void HealEnemy(float healAmountPerSecond)
    {
        animator.Play(animaciónCuración);
        health += healAmountPerSecond * Time.deltaTime;
        health = Mathf.Min(health, maxHealth);
    }

    public void PlayAnimationIfHealthBelowZero()
    {
        if (health <= 0)
        {
            if (animator != null)
            {
                animator.Play(enemyDeadAnimationName);
            }
            else
            {
                Debug.LogError("Animator reference not set!");
            }
        }
    }

    // Método para activar la invulnerabilidad temporal
    public void ActivateInvulnerability(float duration)
    {
        if (invulnerabilityCoroutine != null)
        {
            StopCoroutine(invulnerabilityCoroutine);
        }
        invulnerabilityCoroutine = StartCoroutine(InvulnerabilityCoroutine(duration));
    }

    // Corutina para la invulnerabilidad temporal
    private IEnumerator InvulnerabilityCoroutine(float duration)
    {
        additionalDamageActivated = true;
        yield return new WaitForSeconds(duration);
        additionalDamageActivated = false;
    }
}
