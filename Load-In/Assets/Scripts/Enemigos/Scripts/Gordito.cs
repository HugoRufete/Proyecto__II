using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gordito : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public float desiredDistance = 5f; // Distancia a la que el enemigo se quiere mantener del jugador
    public float minDistanceToPlayer = 2f; // Distancia mínima a la que el enemigo debe mantenerse del jugador
    public float movementSpeed = 5f;
    public float projectileChargeTime = 2f; // Tiempo de carga del proyectil
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float projectileCooldown = 2f; // Tiempo de espera entre disparos

    private bool isChargingProjectile = false;//Booleana que comprueba si el enemigo está cargando el projectil o no
    private float chargeTimer = 0f;
    private float lastShotTime = 0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        // Calcula la dirección y distancia entre el jugador y el enemigo
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        bool isMoving = false;

        // Si el enemigo está demasiado cerca del jugador y no está cargando un proyectil, se aleja
        if (distanceToPlayer < minDistanceToPlayer && !isChargingProjectile)
        {
            Vector3 targetPoint = transform.position - directionToPlayer.normalized * minDistanceToPlayer * 1.5f;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);

            isMoving = true;
        }

        if (isMoving && !isChargingProjectile)
        {
            animator.Play("Caminado_G");
        }
        else
        {
            // Si el enemigo no está cargando un proyectil y ha pasado el tiempo de cooldown desde el último disparo
            if (!isChargingProjectile && Time.time - lastShotTime >= projectileCooldown)
            {
                // Si la distancia es mayor que la deseada, mueve al enemigo hacia el punto objetivo
                if (distanceToPlayer > desiredDistance)
                {
                    // Calcula el punto objetivo a lo largo de la línea entre el enemigo y el jugador
                    Vector3 targetPoint = player.position - directionToPlayer.normalized * desiredDistance;

                    // Mueve al enemigo hacia el punto objetivo
                    transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
                }
                else
                {
                    // Inicia la carga del proyectil
                    isChargingProjectile = true;
                    chargeTimer = 0f;
                    animator.Play("Attack_G");
                }
            }
            else
            {
                // Incrementa el temporizador de carga si está cargando un proyectil
                if (isChargingProjectile)
                {
                    chargeTimer += Time.deltaTime;

                    // Si el temporizador de carga alcanza el tiempo deseado, lanza el proyectil
                    if (chargeTimer >= projectileChargeTime)
                    {
                        // Instancia el proyectil en la posición del enemigo
                        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                        // Calcula la dirección hacia el jugador
                        Vector3 directionToPlayerNormalized = directionToPlayer.normalized;

                        // Calcula la velocidad del proyectil para mejorar la precisión
                        projectile.GetComponent<Rigidbody2D>().velocity = directionToPlayerNormalized * projectileSpeed;

                        // Reinicia la carga del proyectil y permite que el enemigo se mueva nuevamente
                        isChargingProjectile = false;
                        lastShotTime = Time.time;
                    }
                }
            }
        }

        // Si el enemigo está moviéndose y no está cargando un proyectil ni disparando, activa la animación de caminar

        // Orienta al enemigo hacia el jugador
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
