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

    public AudioClip attackSound;   

    public int damageAmount = 10;
    private VidaPlayer vidaPlayer;
    EnemyHealth healthE;

    // Start is called before the first frame update
    void Start()
    {
        myanimator = GetComponent<Animator>();
        attackCollider.enabled = false;
        onedamage = GetComponent<EnemyDamage>();
        player = GameObject.Find("Player").transform;
        isattacking = false;
        healthE = GetComponent<EnemyHealth>();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (healthE.health <= 0)
        {
            healthE.DestroyObject();
        }
        Vector3 direction = player.position - transform.position;

        if (direction.magnitude > distanciaDeseada && !isattacking)
        {
            direction.Normalize();

            Vector3 desplazamiento = direction * (direction.magnitude - distanciaDeseada);

            transform.Translate(direction * enemySpeed * Time.deltaTime);

            myanimator.SetBool("Canwalk", true);

            attackCollider.enabled = false;

            if (Time.time >= lastTimeAttack + attackCooldown && isattacking)
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

    public void Attaack()
    {
        isattacking = true;
    }

    public void Sound()
    {
        ControladorSonido.Instance.EjecutarSonido(attackSound);
    }

    public void NotAttaack()
    {
        isattacking = false;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            vidaPlayer = other.GetComponent<VidaPlayer>();
            if (vidaPlayer != null)
            {
                // Inflige da�o al jugador
                InflictDamage();
            }
        }
    }

    public void InflictDamage()
    {
        if (isattacking == true)
        {
            isattacking = true;
            // Inflige da�o al jugador
            vidaPlayer.PlayerTakeDamage(damageAmount);
            Debug.Log("Da�o Inflingido");
            isattacking = false;
        }

    }
}
