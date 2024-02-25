using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public float followSpeed = 5f; // Velocidad de seguimiento
    private Transform player; // Referencia al transform del jugador

    private Rigidbody2D rb2d;


    [SerializeField] private float radiousToExplode; //Radio del area de detecciion para la explosi�n
    



    void Start()
    {
        // Encuentra al jugador al comienzo
        player = GameObject.FindWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Verifica que se haya encontrado al jugador
        if (player != null)
        {
            // Calcula la direcci�n hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Mueve al enemigo en la direcci�n del jugador con la velocidad espec�fica
            transform.Translate(direction * followSpeed * Time.deltaTime);
        }


        //Si el radio de explosion es menor a la distancia con el jugador
        if (radiousToExplode <= transform.position.x - player.transform.position.x  && radiousToExplode <= transform.position.y - player.transform.position.y)
        {
            followSpeed = 0f;
            //animaci�n de explotar
            
        }
        
    }
}
