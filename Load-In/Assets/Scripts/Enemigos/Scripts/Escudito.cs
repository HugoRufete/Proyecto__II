using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudito : MonoBehaviour
{
    public Collider2D attackCollider; //Collider de ataque
    public Collider2D standarCollider; //Colllider normal
    public Transform Player; //Ubicación del jugador
    public float velocidadMovimiento = 5.0f; //Velocidad movimeinto enemigo
    public float distanciaDeseada = 1.0f; //Distancia minima que habrá entre el jugador y el enemigo para evitar problemas
    public float AttackCooldown = 1.0f; //Cooldown de ataque
    float lastTimeAttack = 0.0f; //Ultima vez que ha disparado
    Animator myanimator;//Referencia a nuestro animator
    bool mirarDerecha = true; //Booleana para controlar a que lado mira el jugador  
    private EnemyHealth enemHealth;
    bool protegiendo = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").transform;
        myanimator = GetComponent<Animator>();
        attackCollider.enabled = false; //Al empezar el collider del enemigo siempre estará desactivado
        enemHealth = GetComponent<EnemyHealth>();
        standarCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Player.position - transform.position; //La diferencia de posición entre el jugador y el enemigo

        if (direction.magnitude > distanciaDeseada) //Si la longutud entre el enemigo y el jugador es mayor que la distancia deseada:
        {
            direction.Normalize(); //Normalizamos el vector

            
            // multiplicando por la diferencia entre la posición menos la distanciaDeseada (distancia mínima entre ellos)

            transform.Translate(direction * velocidadMovimiento * Time.deltaTime); //Trasladar al enemigo a la dirección a la posición del jugador multiplicando por su velocidad 
            //Time.DeltaTime(para que vaya igual en todos los ordenadores

            myanimator.SetBool("IsWalking", true); //Si la condición de arriba es verdadera activamos la animaciónd de andar
            attackCollider.enabled = false;

            if (Time.time >= lastTimeAttack + AttackCooldown) //Si ya ha pasado el cooldwon llamamos al metodo attack
            {

                Attack();
            }
        }

        else
        {
            myanimator.SetBool("IsWalking", false); //Si no lo es la desactivamos
        }

        if (transform.position.x > Player.position.x && mirarDerecha) //Si la posiciíon del enemigo es mayor en el eje x que la del jugador y 
            //la booleana es verdadera llamamos al método voltear(flipear al jugador)
        {
            // Voltear al enemigo
            Voltear();
        }
        else if (transform.position.x < Player.position.x && !mirarDerecha) //Si la posiciíon del enemigo es menor en el eje x que la del jugador y 
            //la booleana es falsa llamamos al método voltear(flipear al jugador)
        {
            // Voltear al enemigo
            Voltear();
        }

        if (enemHealth != null && enemHealth.health < 20 && !protegiendo)
        {
            Debug.Log("Empieza la corrutina");
            StartCoroutine(Proteger4segundos());
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
        myanimator.SetBool("IsAttacking", true); //Activamos la animación de atacar

      
        lastTimeAttack = Time.time; // Confirmamos que hemos atacado para poder empezar el cooldown
    }

    public void EnableCollider()
    {
        attackCollider.enabled = true; //Activamos el collider a principios del ataque llamando a este método mediante un evento en la animación
        
    }

    public void DisableCollider()
    {
        attackCollider.enabled = false; //Desactivamos el collider al final del ataque para que no haga daño al enemigo constantemente y solo lo haga una vez por ataque
    }
    IEnumerator Proteger4segundos()
    {
        protegiendo = true;

        if (enemHealth != null && enemHealth.health < 20)
        {
            Debug.Log("Animación protegerse");
            Debug.Log("MustProtect activado: " + myanimator.GetBool("MustProtect"));
            myanimator.SetBool("MustProtect", true);
            standarCollider.enabled = false;

        }

        yield return new WaitForSeconds(4);
        myanimator.SetBool("MustProtect", false);
        protegiendo = false;
        standarCollider.enabled = true;
        Debug.Log("Se activa collider de nuevo");
    }

}
