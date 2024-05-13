using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gordito : MonoBehaviour
{
    public Transform player;
    public bool mirarDerecha = true;
    public float distanciaDeseada;
    public float cooldownAttack = 2.0f;
    public float speed;
    bool isAttacking;
    bool finishedAttack;
    EnemyHealth enemyH;
    public GameObject projectilePrefab;

    Animator myAnimator;

    float lastTimeAttack;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        myAnimator = GetComponent<Animator>();
        isAttacking = false;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;

        if (enemyH != null && enemyH.health <= 0)
        {
            // Detener al personaje si el enemigo está muerto
            myAnimator.SetBool("IsWalking", false);
            return; // Salir de la función Update() para evitar que el personaje siga moviéndose
        }

        if (direction.magnitude > distanciaDeseada && !isAttacking)
        {
            direction.Normalize();

            transform.Translate(direction * speed * Time.deltaTime);

            myAnimator.SetBool("IsWalking", true);

            if (Time.time > lastTimeAttack + cooldownAttack)
            {
                Attack();
            }

            else
            {
                myAnimator.SetBool("IsAttacking", false);
            }
        }

        else
        {
            myAnimator.SetBool("IsWalking", false);
        }

        UpdateDirection();
    }

    void UpdateDirection()
    {
        if (transform.position.x < player.position.x && mirarDerecha)
        {
            Flip();
        }

        else if (transform.position.x > player.position.x && !mirarDerecha)
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 nuevaEscala = transform.localScale;
        nuevaEscala.x *= -1f;
        transform.localScale = nuevaEscala;
        mirarDerecha = !mirarDerecha;
    }

    void Attack()
    {
        isAttacking = true;
        lastTimeAttack = Time.time;
        isAttacking = false;
       
    }

    public void Proyectil()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
    }

    public void IsAttacking()
    {
        isAttacking = true;
    }

    public void IsNotAttacking()
    {
        isAttacking = false;
    }

}

