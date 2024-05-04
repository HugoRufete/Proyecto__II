using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentaculin : MonoBehaviour
{
    public Transform player;
    public float distanciaDeseada = 1.0f;
    public float enemySpeed;
    Animator myanimator;
    public Collider2D standarCollider;
    public Collider2D attackCollider;
    float lastTimeAttack = 0.0f;
    public float attackCooldown = 2.0f;
    bool mirarDerecha = true;
    private EnemyDamage onedamage;

    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
        attackCollider.enabled = false;
        onedamage = GetComponent<EnemyDamage>();
        player = GameObject.Find("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;

        if (direction.magnitude > distanciaDeseada)
        {
            direction.Normalize();

            Vector3 desplazamiento = direction * (direction.magnitude - distanciaDeseada);

            transform.Translate(direction * enemySpeed * Time.deltaTime);

            myanimator.SetBool("Canwalk", true);

            attackCollider.enabled = false;

            if (Time.time >= lastTimeAttack + attackCooldown)
            {
                Attack();
            }


        }

        else
        {
            myanimator.SetBool("Canwalk", false);
        }

        if (transform.position.x < player.position.x && mirarDerecha)
        {
            Voltear();
        }

        else if (transform.position.x > player.position.x && !mirarDerecha)
        {
            Voltear();
        }

        if (attackCollider.enabled == true && onedamage != null)
        {
            onedamage.InflictDamage();
        }

    }

    void Voltear()
    {
        // Invertir la escala en el eje X para voltear al enemigo
        Vector3 nuevaEscala = transform.localScale;
        nuevaEscala.x *= -1;
        transform.localScale = nuevaEscala;

        // Cambiar la dirección del enemigo
        mirarDerecha = !mirarDerecha;
    }

    void Attack()
    {
       myanimator.SetBool("CanAttack", true);

        lastTimeAttack = Time.time;
    }

    public void EnableCollider()
    {
        attackCollider.enabled = true;
        
        
    }

    public void DisableCollider()
    {
        attackCollider.enabled = false;
    }
}
