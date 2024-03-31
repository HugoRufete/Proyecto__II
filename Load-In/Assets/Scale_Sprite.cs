using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale_Sprite : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        // Encuentra el transform del jugador al inicio
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Si el jugador está a la derecha del objeto
        if (playerTransform.position.x > transform.position.x)
        {
            // Escala en x = -1
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        // Si el jugador está a la izquierda del objeto
        else if (playerTransform.position.x < transform.position.x)
        {
            // Escala en x = 1
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
