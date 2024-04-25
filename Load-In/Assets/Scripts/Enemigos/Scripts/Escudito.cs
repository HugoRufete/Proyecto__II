using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudito : MonoBehaviour
{

    public Transform Player;
    public float velocidadMovimiento = 5.0f;
    public float distanciaDeseada = 1.0f;
    public float AttackCooldown = 2.0f;
    float lastTimeAttack = 0f;
    Animator myanimator;
    bool mirarDerecha = true;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        myanimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.position - transform.position;

        if (direction.magnitude > distanciaDeseada)
        {
            direction.Normalize();

            Vector3 desplazamiento = direction * (direction.magnitude - distanciaDeseada);

            transform.Translate(direction * velocidadMovimiento * Time.deltaTime);

            myanimator.SetBool("IsWalking", true);
        }

        else
        {
            myanimator.SetBool("IsWalking", false);
        }

        if (transform.position.x > Player.position.x && mirarDerecha)
        {
            // Voltear al enemigo
            Voltear();
        }
        else if (transform.position.x < Player.position.x && !mirarDerecha)
        {
            // Voltear al enemigo
            Voltear();
        }

        if (Time.time >= lastTimeAttack + AttackCooldown)
        {
            
            Attack();
        }
            
            
    }

    void Voltear()
    {
        // Invertir la escala en el eje X para voltear al enemigo
        Vector3 nuevaEscala = transform.localScale;
        nuevaEscala.x *= -1;
        transform.localScale = nuevaEscala;

        // Cambiar la direcci�n del enemigo
        mirarDerecha = !mirarDerecha;
    }

    void Attack()
    {
        myanimator.SetBool("IsAttacking", true);

      
        lastTimeAttack = Time.time;
    }
}
