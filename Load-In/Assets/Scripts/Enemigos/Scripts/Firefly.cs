using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    private Transform player; // Referencia al transform del jugador
    private EnemyFollow enemyFollow;//Referencia al script de movimiento de los enemigos
    


    [SerializeField] private float radiousToExplode; //Radio del area de deteccion para la explosión
    



    void Start()
    {
        // Encuentra al jugador al comienzo
        player = GameObject.FindWithTag("Player").transform;
        //Llama al script de movimiento del enemigo
        enemyFollow = GetComponent<EnemyFollow>();
        
    }

    void Update()
    {
        // Verifica que se haya encontrado al jugador
        if (player != null)
        {
            //Si el radio de explosion es menor a la distancia con el jugador
            if (radiousToExplode >= Vector3.Distance(transform.position, player.position))
            {
                enemyFollow.followSpeed = 3;
                //animación de explotar
                Debug.Log("Exploto");

            }
        }
    }
}
