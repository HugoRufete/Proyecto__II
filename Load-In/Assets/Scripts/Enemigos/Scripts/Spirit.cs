using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    public float desiredDistance = 5f; // Distancia a la que el enemigo se quiere mantener del jugador
    public float movementSpeed = 5f;
    public float healRadius = 2f; // Radio del �rea de curaci�n
    public float healAmountPerSecond = 10f; // Cantidad de curaci�n por segundo

    private Transform player; // La variable player ya no es p�blica

    void Start()
    {
        // Busca el GameObject con el nombre "Player" y obtiene su Transform
        player = GameObject.Find("Player").transform;

        // Comprueba si se ha encontrado el jugador
        if (player == null)
        {
            Debug.LogError("No se ha encontrado el GameObject con el nombre 'Player'. Aseg�rate de que existe y tiene el nombre correcto.");
        }
    }

    void Update()
    {
        // Si el jugador no ha sido encontrado, no realiza ninguna acci�n
        if (player == null)
            return;

        // Calcula la cantidad de curaci�n por fotograma
        float healPerFrame = healAmountPerSecond * Time.deltaTime;

        // Verifica si hay enemigos dentro del �rea de curaci�n y cura a todos los que encuentre
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, healRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    // Curar al enemigo con la cantidad de curaci�n por fotograma
                    enemyHealth.HealEnemy(healAmountPerSecond);
                }
                else
                {
                    Debug.LogWarning("El objeto dentro del �rea de curaci�n no tiene un componente EnemyHealth adjunto.");
                }
            }
        }

        // Calcula la direcci�n y distancia entre el jugador y el enemigo
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // Si la distancia es mayor que la deseada, mueve al enemigo hacia el punto objetivo
        if (distanceToPlayer > desiredDistance)
        {
            // Calcula el punto objetivo a lo largo de la l�nea entre el enemigo y el jugador
            Vector3 targetPoint = player.position - directionToPlayer.normalized * desiredDistance;

            // Mueve al enemigo hacia el punto objetivo
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
        }
    }

    // Este m�todo dibuja el �rea de curaci�n en el editor de Unity para una f�cil visualizaci�n
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
}
