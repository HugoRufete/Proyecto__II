using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuadañaEnemy : MonoBehaviour
{
    private Transform player;
    public GameObject projectilePrefab;
    private EnemyHealth Health;
    public float desiredDistance = 5f;
    public float minDistanceToPlayer = 2f;
    public float movementSpeed = 5f;
    public float projectileChargeTime = 2f;
    public float projectileSpeed = 10f;
    public float projectileCooldown = 2f;

    private bool isChargingProjectile = false;
    private float chargeTimer = 0f;
    private float lastShotTime = 0f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        Health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if(Health.health > 0)
        {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer < minDistanceToPlayer && !isChargingProjectile)
        {
            Vector3 targetPoint = transform.position - directionToPlayer.normalized * minDistanceToPlayer * 1.5f;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
            animator.Play("Guadaña_Caminando");
        }
        else
        {
            if (!isChargingProjectile && Time.time - lastShotTime >= projectileCooldown)
            {
                if (distanceToPlayer > desiredDistance)
                {
                    Vector3 targetPoint = player.position - directionToPlayer.normalized * desiredDistance;
                    transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
                    animator.Play("Guadaña_Caminando");
                }
                else
                {
                    isChargingProjectile = true;
                    chargeTimer = 0f;
                }
            }
            else
            {
                if (isChargingProjectile)
                {
                    chargeTimer += Time.deltaTime;
                    animator.Play("Guadaña_Ataque_Rango");

                    if (chargeTimer >= projectileChargeTime)
                    {
                        Vector3 directionToPlayerNormalized = directionToPlayer.normalized;
                        Vector3 projectileSpawnOffset = directionToPlayerNormalized.x < 0 ? new Vector3(-0.5f, 0.8f, 0f) : new Vector3(0.5f, 0.8f, 0f);
                        Vector3 projectileSpawnPosition = transform.position + projectileSpawnOffset;
                        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition, Quaternion.identity);

                        // Ajustar la escala del proyectil según la dirección del enemigo
                        Vector3 projectileScale = projectile.transform.localScale;
                        projectileScale.x = directionToPlayerNormalized.x < 0 ? 1f : -1f;
                        projectile.transform.localScale = projectileScale;

                        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
                        projectileRigidbody.velocity = directionToPlayerNormalized * projectileSpeed;

                        animator.Play("Guadaña_Ataque_Rango");

                        isChargingProjectile = false;
                        lastShotTime = Time.time;
                    }
                }
            }
        }

        if (transform.position.x > player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    }
}
