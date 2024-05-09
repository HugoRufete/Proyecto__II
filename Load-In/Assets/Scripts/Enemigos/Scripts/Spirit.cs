using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spirit : MonoBehaviour
{
    private Transform player;
    public float desiredDistance = 5f; // Distancia a la que el enemigo se quiere mantener del jugador
    public float movementSpeed = 5f;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        // Calcula la dirección y distancia entre el jugador y el enemigo
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

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
            // Calcula el punto objetivo a lo largo de la línea entre el enemigo y el jugador
            Vector3 targetPoint = player.position - directionToPlayer.normalized * desiredDistance;

            // Mueve al enemigo hacia el punto objetivo
            transform.position = Vector3.MoveTowards(transform.position, targetPoint, movementSpeed * Time.deltaTime);
        }

        if (transform.position.x > player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        if (transform.position.x < player.position.x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
