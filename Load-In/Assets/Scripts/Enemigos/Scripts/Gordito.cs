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

