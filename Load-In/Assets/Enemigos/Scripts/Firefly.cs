using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public float followSpeed = 5f; // Velocidad de seguimiento
    private Transform player; // Referencia al transform del jugador

    


    [SerializeField] private float radiousToExplode; //Radio del area de deteccion para la explosión
    



    void Start()
    {
        // Encuentra al jugador al comienzo
        player = GameObject.FindWithTag("Player").transform;
        
    }

    void Update()
    {
        // Verifica que se haya encontrado al jugador
        if (player != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo en la dirección del jugador con la velocidad específica
            transform.Translate(direction * followSpeed * Time.deltaTime);
        }


        //Si el radio de explosion es menor a la distancia con el jugador
        if (radiousToExplode <= transform.position.x - player.transform.position.x  && radiousToExplode <= transform.position.y - player.transform.position.y)
        {
            followSpeed = 0f;
            //animación de explotar
            
        }
        
    }
}
