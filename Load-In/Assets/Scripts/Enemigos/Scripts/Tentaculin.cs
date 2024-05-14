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
    bool isattacking = false;
    EnemyHealth enemHealth;

    public int damageAmount = 10;
    private VidaPlayer vidaPlayer;

    public float distanciaAgarre;

    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
        attackCollider.enabled = false;
        onedamage = GetComponent<EnemyDamage>();
        player = GameObject.Find("Player").transform;
        isattacking = false;
        enemHealth = GetComponent<EnemyHealth>();
    }


    // Update is called once per frame
    void Update()
    {
        Destroy();

        Vector3 direction = player.position - transform.position;

        if (direction.magnitude > distanciaDeseada && !isattacking)
        {
            direction.Normalize();

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


        if (direction.magnitude > distanciaAgarre)
        {
            Debug.Log("Desaparecer");
            myanimator.SetBool("CanWalk", false);
            myanimator.Play("tentaculin_catch");

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
            // onedamage.InflictDamage();
        }

    }

    public void isAttacking()
    {
        isattacking = true;
    }

    public void isNotAttacking()
    {
        isattacking = false;
    }

    public void SetFalse()
    {
        player.gameObject.SetActive(false);
    }

    public void SetTrue()
    {
        player.gameObject.SetActive(true);
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
        isattacking = true;
    }

    public void DisableCollider()
    {
        attackCollider.enabled = false;
        isattacking = false;
    }

    public void Destroy()
    {
        if (enemHealth != null && enemHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vidaPlayer = other.GetComponent<VidaPlayer>();
            if (vidaPlayer != null)
            {
                // Inflige daño al jugador
                InflictDamage();
            }
        }
    }

    public void InflictDamage()
    {
        if (isattacking == true)
        {
            isattacking = true;
            // Inflige daño al jugador
            vidaPlayer.PlayerTakeDamage(damageAmount);
            Debug.Log("Daño Inflingido");
            isattacking = false;
        }

    }
}
