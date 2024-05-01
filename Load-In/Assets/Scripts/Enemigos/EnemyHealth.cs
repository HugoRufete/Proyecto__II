using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public float maxHealth = 20;
    private bool invulnerable = false;
    public bool additionalDamageActivated = false;
    private float additionalDamageMultiplier = 2.0f;
    
    public GameObject experiencePrefab;
    public GameObject esporaPrefab;
    public GameObject curaPrefab;
    public GameObject ammoPrefab;

    [Header("Esporas")]
    private int cantidadItemsDropeados = 3;
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

    private void Awake()
    {
        // Registrar el enemigo en la lista de enemigos del AdditionalDamageController
        if (AumentarDañoAEnemigos.Instance != null)
        {
            AumentarDañoAEnemigos.Instance.enemyHealthList.Add(this);
        }
    }
    private void Start()
    {
        health = maxHealth;
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void EnemyTakeDamage(float damageAmount)
    {
        if (additionalDamageActivated)
        {
            ApplyAdditionalDamage(damageAmount);
        }
        else if (!invulnerable)
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
        GameObject prefab1 = experiencePrefab;
        GameObject prefab2 = esporaPrefab;
        GameObject prefab3 = curaPrefab; 
        GameObject prefab4 = ammoPrefab;

        for (int i = 0; i < cantidadItemsDropeados * 2; i++)
        {
            if (i % 2 == 0)
            {
                prefab1 = experiencePrefab;
                prefab2 = esporaPrefab;
                prefab3 = curaPrefab;
                prefab4 = ammoPrefab;
            }

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector2 spawnPosition = enemyPosition + randomDirection * Random.Range(0f, maxDistanceFromEnemy);

            if (i % 2 == 0)
            {
                if (Random.value < 0.05) // Probabilidad del 5%
                {
                    Instantiate(prefab3, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Instantiate(prefab1, spawnPosition, Quaternion.identity);
                }
            }
            else
            {
                if (Random.value < 0.10) // Probabilidad del 10%
                {
                    Instantiate(prefab4, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Instantiate(prefab2, spawnPosition, Quaternion.identity);
                }
            }
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
        invulnerable = true;
        yield return new WaitForSeconds(duration);
        invulnerable = false;
    }

    public void ApplyAdditionalDamage(float damageAmount)
    {
        health -= damageAmount * additionalDamageMultiplier;

    }
}
