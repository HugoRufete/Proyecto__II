using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    public float desiredDistance = 5f; // Distancia a la que el enemigo se quiere mantener del jugador
    public float movementSpeed = 5f;
    public float healRadius = 2f; // Radio del �rea de curaci�n
    public float healAmountPerSecond = 5f; // Cantidad de curaci�n por segundo

    private Transform player; // La variable player ya no es p�blica

    private Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        animator = GetComponent<Animator>();

        // Comprueba si se ha encontrado el jugador
        if (player == null)
        {
            Debug.LogError("No se ha encontrado el GameObject con el nombre 'Player'. Aseg�rate de que existe y tiene el nombre correcto.");
        }
    }

    void Update()
    {
        if (player == null)
            return;

        animator.Play("Spirit_Curaci�n");

        float healPerFrame = healAmountPerSecond * Time.deltaTime;
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

        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > desiredDistance)
        {
            Vector3 targetPoint = player.position - directionToPlayer.normalized * desiredDistance;

            transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRadius);
    }
}
