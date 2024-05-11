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
    bool stopPlayer;
    bool isAttacking;

    public GameObject projectilePrefab;

    Animator myAnimator;

    float lastTimeAttack;

    void Start()
    {
        player = GameObject.Find("Player").transform;
        myAnimator = GetComponent<Animator>();
        stopPlayer = false;
        isAttacking = false;
    }

    void Update()
    {
        Vector3 direction = player.position - transform.position;

        if (!isAttacking && direction.magnitude > distanciaDeseada)
        {
            Move(direction);
            if (Time.time > lastTimeAttack + cooldownAttack)
            {
                Attack();
            }
        }
        else
        {
            StopMoving();
        }

        UpdateDirection();

        if (stopPlayer)
        {
            speed = 0;
        }
        else if (!stopPlayer)
        {
            speed = 5;
        }


    }

    void Move(Vector3 direction)
    {
        direction.Normalize();
        transform.Translate(direction * speed * Time.deltaTime);
        myAnimator.SetBool("IsWalking", true);
    }

    void StopMoving()
    {
        myAnimator.SetBool("IsWalking", false);
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
        stopPlayer = true;
        myAnimator.SetBool("IsAttacking", true);
        isAttacking = true;
        lastTimeAttack = Time.time;

    }

    public void LaunchProjectile()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            ProyectilGordito projectileScript = projectile.GetComponent<ProyectilGordito>();
            if (projectileScript != null)
            {
                projectileScript.target = player;
            }
        }
    }

    public void FinishAttack()
    {
        myAnimator.SetBool("IsAttacking", false);
        isAttacking = false;
        stopPlayer = false;
    }
}
