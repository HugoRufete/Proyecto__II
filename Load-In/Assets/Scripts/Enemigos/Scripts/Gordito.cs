using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gordito : MonoBehaviour
{
    private Transform player;
    public GameObject projectilePrefab;
    public float desiredDistance = 5f; // Distancia a la que el enemigo se quiere mantener del jugador
    public float minDistanceToPlayer = 2f; // Distancia m�nima a la que el enemigo debe mantenerse del jugador
    public float movementSpeed = 5f;
    public float projectileChargeTime = 2f; // Tiempo de carga del proyectil
    public float projectileSpeed = 10f; // Velocidad del proyectil
    public float projectileCooldown = 2f; // Tiempo de espera entre disparos

    private bool isChargingProjectile = false; // Booleana que comprueba si el enemigo est� cargando el projectil o no
    private float chargeTimer = 0f;
    private float lastShotTime = 0f;
    private Animator animator; // Referencia al componente Animator

    void Start()
    {
        // Obtener el componente Animator al iniciar
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // Calcula la direcci�n y distancia entre el jugador y el enemigo
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Si el enemigo est� demasiado cerca del jugador y no est� cargando un proyectil, se aleja
        if (distanceToPlayer < minDistanceToPlayer && !isChargingProjectile)
        {
            Vector3 targetPoint = transform.position - directionToPlayer.normalized * minDistanceToPlayer * 1.5f;
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
            // Aqu� deber�as cambiar la animaci�n a "Walk_G"
            animator.Play("Walk_G");
        }
        else
        {
            // Si el enemigo no est� cargando un proyectil y ha pasado el tiempo de cooldown desde el �ltimo disparo
            if (!isChargingProjectile && Time.time - lastShotTime >= projectileCooldown)
            {
                // Si la distancia es mayor que la deseada, mueve al enemigo hacia el punto objetivo
                if (distanceToPlayer > desiredDistance)
                {
                    // Calcula el punto objetivo a lo largo de la l�nea entre el enemigo y el jugador
                    Vector3 targetPoint = player.position - directionToPlayer.normalized * desiredDistance;

                    // Mueve al enemigo hacia el punto objetivo
                    transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
                    // Aqu� deber�as cambiar la animaci�n a "Walk_G"
                    animator.Play("Walk_G");
                }
                else
                {
                   
                    // Inicia la carga del proyectil
                    isChargingProjectile = true;
                    chargeTimer = 0f;

                    animator.Play("Guada�a_Ataque");
                }
            }
            else
            {
                // Incrementa el temporizador de carga si est� cargando un proyectil
                if (isChargingProjectile)
                {
                    chargeTimer += Time.deltaTime;

                    // Si el temporizador de carga alcanza el tiempo deseado, lanza el proyectil
                    if (chargeTimer >= projectileChargeTime)
                    {
                        // Obtener la direcci�n hacia el jugador y normalizar
                        Vector3 directionToPlayerNormalized = directionToPlayer.normalized;

                        // Calcular la posici�n de instancia del proyectil
                        Vector3 projectileSpawnOffset = directionToPlayerNormalized.x < 0 ? new Vector3(-0.5f, 0.8f, 0f) : new Vector3(0.5f, 0.8f, 0f);
                        Vector3 projectileSpawnPosition = transform.position + projectileSpawnOffset;

                        // Instancia el proyectil en la posici�n calculada
                        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition, Quaternion.identity);

                        // Establecer la velocidad del proyectil directamente hacia el jugador
                        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
                        projectileRigidbody.velocity = directionToPlayerNormalized * projectileSpeed;

                        // Reiniciar la carga del proyectil y permitir que el enemigo se mueva nuevamente
                        isChargingProjectile = false;
                        lastShotTime = Time.time;
                    }
                }
            }
        }

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
